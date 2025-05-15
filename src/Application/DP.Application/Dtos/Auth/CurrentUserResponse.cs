using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP.Application.Dtos.Auth;

public record CurrentUserResponse(
    string UserId,
    string UserName,
    string Email,
    string FirstName,
    string LastName,
    string Gender,
    bool? IsAuthenticated);