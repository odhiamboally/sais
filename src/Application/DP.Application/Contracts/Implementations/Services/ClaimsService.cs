using DP.Application.Contracts.Abstractions.IServices;
using DP.Domain.Entities;

using Microsoft.AspNetCore.Identity;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DP.Application.Contracts.Implementations.Services;
internal sealed class ClaimsService : IClaimsService
{
    private readonly UserManager<AppUser> _userManager;

    public ClaimsService(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
            
    }

    public async Task<List<Claim>> GetUserClaimsAsync(AppUser appUser)
    {
        try
        {
            List<Claim> userClaims = new()
                {
                    new Claim("Id", Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.NameIdentifier, appUser.Id.ToString()),
                    new Claim("Username", appUser.UserName!),
                    new Claim("Password", appUser.PasswordHash!),
                    new Claim(ClaimTypes.Name, appUser.FullName!),
                    new Claim(ClaimTypes.Email, appUser.Email!)
                };

            var userRoles = await _userManager.GetRolesAsync(appUser);

            foreach (var userRole in userRoles)
            {
                userClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            return userClaims;
        }
        catch (Exception)
        {
            throw;
        }
    }
}
