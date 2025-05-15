using DP.Application.Dtos.Common;
using DP.Domain.Entities;

using Microsoft.IdentityModel.Tokens;

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DP.Application.Contracts.Abstractions.IServices;
public interface IJwtService
{
    JwtSecurityToken GetJwtToken(List<Claim> userClaims);
    Task<ApiResponse<string>> GenerateToken(AppUser user);
    bool IsTokenValid(SecurityToken token);
}
