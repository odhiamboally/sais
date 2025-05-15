using DP.Application.Configuration;
using DP.Application.Contracts.Abstractions.IServices;
using DP.Application.Dtos.Common;
using DP.Application.Utilities;
using DP.Domain.Entities;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DP.Application.Contracts.Implementations.Services;
internal sealed class JwtService : IJwtService
{
    private readonly JwtSettings _jwtSettings;
    private readonly UserManager<AppUser> _userManager;

    public JwtService(
        IOptions<JwtSettings> jwtSettings, 
        UserManager<AppUser> userManager)
    {
        _jwtSettings = jwtSettings.Value;
        _userManager = userManager;
    }

    public async Task<ApiResponse<string>> GenerateToken(AppUser user)
    {
        var roles = await _userManager.GetRolesAsync(user);

        var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, user.Id),
        new Claim(ClaimTypes.Email, user.Email!),
        new Claim(ClaimTypes.Name, user.UserName!),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())

    };

        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret!));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationInMinutes), // ✅ Use UtcNow
            signingCredentials: credentials
        );

        return ApiResponse<string>.Success("Success", new JwtSecurityTokenHandler().WriteToken(token));
    }

    public JwtSecurityToken GetJwtToken(List<Claim> userClaims)
    {
        try
        {
            AuthExtensions.SecurityKey(out string key);

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.JwtSecurityKey!));

            var expiryInMin = Convert.ToInt32(_jwtSettings.ExpirationInMinutes);

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: userClaims,
                expires: DateTime.UtcNow.AddMinutes(expiryInMin), // ✅ Changed DateTimeOffset.UtcNow to DateTime.UtcNow
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            token.Header.Add("kid", authSigningKey.Key);
            token.SigningKey = authSigningKey;
            _jwtSettings.SignKey = authSigningKey;
            return token;

        }
        catch (Exception ex)
        {
            throw new Exception($"Error fetching JwtToken | {ex.InnerException}");
        }
    }

    public bool IsTokenValid(SecurityToken token)
    {
        try
        {
            if (token == null)
            {
                throw new ArgumentException("Token is null");
            }

            AuthExtensions.SecurityKey(out string key);
            var clockSkew = Convert.ToDouble(_jwtSettings.ClockSkew);
            TokenValidationParameters tokenValidationParameters = new TokenValidationParameters
            {
                ClockSkew = TimeSpan.FromMinutes(clockSkew),
                ValidIssuer = _jwtSettings.Issuer,
                ValidAudience = _jwtSettings.Audience,
                IssuerSigningKey = token.SigningKey,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,

            };

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

            try
            {
                ClaimsPrincipal tokenValid = new JwtSecurityTokenHandler().ValidateToken(jwtSecurityTokenHandler.WriteToken(token), tokenValidationParameters, out var securityToken);

                return true;
            }
            catch (SecurityTokenValidationException ex)
            {
                throw new SecurityTokenValidationException($"Error|Token validation failed|{ex.Message}");
            }

        }
        catch (Exception)
        {
            throw;
        }
    }
}

