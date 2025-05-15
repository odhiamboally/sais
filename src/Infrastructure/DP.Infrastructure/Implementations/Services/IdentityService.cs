
using DP.Application.Configuration;
using DP.Application.Contracts.Abstractions.IServices;
using DP.Application.Dtos.Auth;
using DP.Application.Dtos.Common;
using DP.Domain.Entities;
using DP.Domain.Interfaces;

using DP.Application.Contracts.Abstractions.Interfaces;
using DP.Domain.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

using static System.Runtime.InteropServices.JavaScript.JSType;
using DP.Domain.Exceptions;

namespace DP.Infrastructure.Implementations.Services;
internal sealed class IdentityService : IIdentityService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IJwtService _jwtService;
    private readonly IClaimsService _claimsService;
    private readonly IEmailService _emailService;
    private readonly EmailSettings _emailSettings;
    private readonly IUnitOfWork _unitOfWork; 


    public IdentityService(
        UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager,
        IHttpContextAccessor contextAccessor,
        IJwtService jwtService,
        IClaimsService claimsService,
        IEmailService emailService,
        IOptions<EmailSettings> emailSettings,
        IUnitOfWork unitOfWork

        )
    {
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
        _httpContextAccessor = contextAccessor ?? throw new ArgumentNullException(nameof(contextAccessor));
        _jwtService = jwtService ?? throw new ArgumentNullException(nameof(jwtService));
        _claimsService = claimsService ?? throw new ArgumentNullException(nameof(claimsService));
        _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
        _emailSettings = emailSettings.Value ?? throw new ArgumentNullException(nameof(emailSettings));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            
    }

    public async Task<ApiResponse<UserResponse>> CreateAsync(string serviceName, CreateUserRequest createUserRequest, string scheme, string host)
    {
        try
        {
            // Check if user is already registered
            var existingUser = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == createUserRequest.Email);

            if (existingUser != null)
            {
                return ApiResponse<UserResponse>.Failure("You are already registered. Please log in.");
            }

            // Create the new user
            var user = new AppUser
            {
                UserName = createUserRequest.UserName,
                Email = createUserRequest.Email
            };

            user.PasswordHash = new PasswordHasher<AppUser>().HashPassword(user, createUserRequest.Password);

            var result = await _userManager.CreateAsync(user, createUserRequest.Password);
            if (!result.Succeeded)
            {
                var errorMessages = result.Errors.Select(e => e.Description).ToList();
                return ApiResponse<UserResponse>.Failure($"User registration failed. Errors | {errorMessages}");
            }

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            var confirmationLink = $"{scheme}://{host}/reset-password?userId={user.Id}&token={token}";

            await _emailService.SendEmailAsync(new SendEmailRequest
            {
                To = user.Email,
                Subject = "Email Confirmation",
                Body = $"<p>Please confirm your account by clicking here:</p>" +
                       $"<p><a href='{confirmationLink}'>Confirm Email</a></p>",
            });

            return ApiResponse<UserResponse>.Success("Success", new UserResponse(UserId: user.Id, Token: token));
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<ApiResponse<ForgotPasswordResponse>> ForgotPasswordAsync(ForgotPasswordRequest model)
    {
        try
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return ApiResponse<ForgotPasswordResponse>.Failure("If the email exists, a reset link has been sent.");
            }

            if (!await _userManager.IsEmailConfirmedAsync(user))
            {
                return ApiResponse<ForgotPasswordResponse>.Failure("f the email exists, a reset link has been sent.");
            }

            string passwordResetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

            var scheme = _httpContextAccessor.HttpContext.Request.Scheme;
            var host = _httpContextAccessor.HttpContext.Request.Host.ToString();

            var resetLink = $"{scheme}://{host}/reset-password?email={Uri.EscapeDataString(user.Email!)}&token={Uri.EscapeDataString(passwordResetToken)}";

            var templateData = new Dictionary<string, string>
            {
                { "Name", user.FirstName! },
                { "Subject", "Reset Your Password" },
                { "ResetLink", resetLink },
                { "CompanyName", _emailSettings.DisplayName! },
                { "Footer", _emailSettings.DisplayName! }
                
                
            };

            string emailBody = await _emailService.ApplyTemplateAsync("PasswordReset", templateData);

            await _emailService.SendEmailAsync(
                new SendEmailRequest
                {
                    To = user.Email,
                    Subject = "Staff Portal Reset password Confirmation Link",
                    Body =
                        $"<p>Reset your password by clicking the link below:</p>"
                        + $"<p><a href='{resetLink}'>Reset Password</a></p>",

                }
            );

            return ApiResponse<ForgotPasswordResponse>.Success("If the email exists, a reset link has been sent.", new ForgotPasswordResponse(
                Token: passwordResetToken,
                ResetLink: resetLink

                ));

        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<ApiResponse<CurrentUserResponse>> GetCurrentUserAsync()
    {
        try
        {
            var userName = _httpContextAccessor.HttpContext?.User?.Identity?.Name;

            if (string.IsNullOrWhiteSpace(userName))
                return ApiResponse<CurrentUserResponse>.Failure("User not authenticated");

            var userId = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return ApiResponse<CurrentUserResponse>.Failure("User not found.");
            }

            var appUser =  await _userManager.FindByIdAsync(userId);
            if (appUser == null)
            {
                return ApiResponse<CurrentUserResponse>.Failure("User not found.");
            }

            bool? IsAuthenticated = _httpContextAccessor.HttpContext?.User.Identity?.IsAuthenticated;
        
            return ApiResponse<CurrentUserResponse>.Success("CurrentUser", new CurrentUserResponse(
                userId, 
                appUser.UserName!, 
                appUser.Email!, 
                appUser.FirstName!, 
                appUser.LastName!, 
                appUser.Gender!,
                IsAuthenticated));
        }
        catch (Exception)
        {

            throw;
        }
    }
    
    public async Task<ApiResponse<LoginResponse>> LoginAsync(LoginRequest loginRequest)
    {
        try
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == loginRequest.UserName);

            if (user == null)
                return ApiResponse<LoginResponse>.Failure("Invalid Employee Number or password.");

            bool passwordValid = await _userManager.CheckPasswordAsync(user, loginRequest.Password);
            if (!passwordValid)
                return ApiResponse<LoginResponse>.Failure("Invalid Employee Number or password.");

            if (!await _userManager.IsEmailConfirmedAsync(user))
                return ApiResponse<LoginResponse>.Failure("Please confirm your email before logging in.");

            // Check if Account is Locked
            var result = await _signInManager.PasswordSignInAsync(user, loginRequest.Password, loginRequest.RememberMe, true);

            if (!result.Succeeded)
                return ApiResponse<LoginResponse>.Failure("Invalid login attempt.");

            if (result.IsLockedOut)
                return ApiResponse<LoginResponse>.Failure("Your account is locked due to multiple failed login attempts. Please reset your password or contact support.");

            var userClaims = await _claimsService.GetUserClaimsAsync(user);

            var userToken = _jwtService.GetJwtToken(userClaims);
            var token = await _jwtService.GenerateToken(user);
            if (token == null)
            {
                return ApiResponse<LoginResponse>.Failure("Could not generate token");
            }

            if (!_jwtService.IsTokenValid(userToken))
            {
                throw new SecurityTokenValidationException($"Error|Token is Invalid");
            }

            if (result.RequiresTwoFactor)
            {
                return ApiResponse<LoginResponse>.Success("User Requires 2 Factor Authentication", new LoginResponse(
                    UserId: user.Id,
                    FirstName: user.FirstName!,
                    LastName: user.LastName!,
                    Email: user.Email!,
                    Requires2FA: true,
                    IsAuthenticated: true,
                    Token: userToken.ToString()!,
                    RefreshToken: string.Empty,
                    UserClaims: userClaims
                    ));
            }

            return ApiResponse<LoginResponse>.Success("Login Successful.", new LoginResponse(
                    UserId: user.Id,
                    FirstName: user.FirstName!,
                    LastName: user.LastName!,
                    Email: user.Email!,
                    Requires2FA: true,
                    IsAuthenticated: true,
                    Token: userToken.ToString()!,
                    RefreshToken: string.Empty,
                    UserClaims: userClaims
                    ));

        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<ApiResponse<bool>> LogoutAsync()
    {
        try
        {
            await _signInManager.SignOutAsync();
            return ApiResponse<bool>.Success("You have been logged out!", true);
        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<ApiResponse<RegisterResponse>> RegisterAsync(string serviceName, RegisterRequest registerRequest)
    {
        try
        {
            var existingUser = await _userManager.Users
                .FirstOrDefaultAsync(x => x.UserName == registerRequest.UserName);

            if (existingUser != null)
                throw new CreatingDuplicateException("You are already registered. Please log in.");
                //return Response<RegisterResponse>.Failure("You are already registered. Please log in.");

            var user = new AppUser
            {
                UserName = existingUser!.UserName,
                Email = existingUser.Email,
                FirstName = existingUser.FirstName,
                MiddleName = existingUser.MiddleName,
                LastName = existingUser.LastName,
                PhoneNumber = existingUser.PhoneNumber,
                Gender = existingUser.Gender,
                IsActive = true
            };

            var result = await _userManager.CreateAsync(user, registerRequest.Password);
            if (!result.Succeeded)
            {
                var errorMessages = result.Errors.Select(e => e.Description).ToList();
                return ApiResponse<RegisterResponse>.Failure($"User registration failed. | Errors: | {errorMessages}");
            }

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            //string encodedToken = WebUtility.UrlEncode(token);

            var scheme = _httpContextAccessor.HttpContext.Request.Scheme;
            var host = _httpContextAccessor.HttpContext.Request.Host.ToString();

            var confirmationLink = $"{scheme}://{host}/reset-password?userId={user.Id}&token={token}";

            await _emailService.SendEmailAsync(new SendEmailRequest
            {
                To = user.Email,
                Subject = "Email Confirmation",
                Body = $"<p>Please confirm your account by clicking here:</p>" +
                        $"<p><a href='{confirmationLink}'>Confirm Email</a></p>",
            });

            return ApiResponse< RegisterResponse>.Success("User registration successful", new RegisterResponse(
                UserId: user.Id,
                Token: token,
                ConfirmationLink: confirmationLink
            ));

        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<ApiResponse<ResetPasswordResponse>> ResetPasswordAsync(ResetPasswordRequest resetPasswordRequest)
    {
        try
        {
            var user = await _userManager.FindByEmailAsync(resetPasswordRequest.Email);

            if (user == null && !string.IsNullOrWhiteSpace(resetPasswordRequest.Email))
            {
                user = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == resetPasswordRequest.Email);
            }

            // Don't reveal user existence for security, but return success to avoid email enumeration
            if (user == null)
                return ApiResponse<ResetPasswordResponse>.Success("Your password has been reset.", new ResetPasswordResponse(false));

            var resetPassResult = await _userManager.ResetPasswordAsync(user, resetPasswordRequest.Token, resetPasswordRequest.Password);

            if (!resetPassResult.Succeeded)
            {
                var errors = string.Join(", ", resetPassResult.Errors.Select(e => e.Description));

                return ApiResponse<ResetPasswordResponse>.Failure($"Password rest failed. | Errors: | {resetPassResult.Errors.Select(e => e.Description)}");
            }


            // Update user properties
            user.EmailConfirmed = true;
            user.AccessFailedCount = 0;
            user.LockoutEnd = null;

            await _userManager.UpdateAsync(user);

            await _emailService.SendEmailAsync(new SendEmailRequest
            {
                To = user.Email,
                Subject = "Your Password Has Been Reset",
                Body = $@"
                <h2>Password Reset Confirmation</h2>
                <p>Hello {user.FirstName ?? user.Email},</p>
                <p>Your password has been successfully reset. If you did not request this change, please contact our support team immediately.</p>
                <p>Best regards,<br>The Support Team</p>"
            });

            return ApiResponse<ResetPasswordResponse>.Success("Password reset sucessfully!", new ResetPasswordResponse(true));
        }
        catch (Exception)
        {

            throw;
        }
    }

    


    #region Private Methods


    #endregion



}
