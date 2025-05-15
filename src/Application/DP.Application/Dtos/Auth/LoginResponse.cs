using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DP.Application.Dtos.Auth;
public record LoginResponse(
    string UserId, 
    string FirstName, 
    string LastName, 
    string Email, 
    bool Requires2FA,
    bool IsAuthenticated,
    string Token,
    string RefreshToken,
    List<Claim>? UserClaims

    );


