using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP.Application.Dtos.Auth;
public record RegisterRequest(
    string UserName,
    string Password,
    string ConfirmPassword
);

