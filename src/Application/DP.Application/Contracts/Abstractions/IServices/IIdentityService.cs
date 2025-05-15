using DP.Application.Dtos.Auth;
using DP.Application.Dtos.Common;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP.Application.Contracts.Abstractions.IServices;
public interface IIdentityService 
{
    
    Task<ApiResponse<UserResponse>> CreateAsync(string serviceName, CreateUserRequest createUserRequest, string scheme, string host);
    Task<ApiResponse<ForgotPasswordResponse>> ForgotPasswordAsync(ForgotPasswordRequest model);
    Task<ApiResponse<CurrentUserResponse>> GetCurrentUserAsync();
    Task<ApiResponse<LoginResponse>> LoginAsync(LoginRequest loginRequest);
    Task<ApiResponse<bool>> LogoutAsync();
    Task<ApiResponse<RegisterResponse>> RegisterAsync(string serviceName, RegisterRequest registerRequest);
    Task<ApiResponse<ResetPasswordResponse>> ResetPasswordAsync(ResetPasswordRequest model);
    
    

}
