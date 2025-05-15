using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP.Application.Dtos.Auth;
public record ResetPasswordRequest(
    string Email,
    string EmployeeNumber,
    string Password,
    string ConfirmPassword,
    string Code,
    string Token

);
