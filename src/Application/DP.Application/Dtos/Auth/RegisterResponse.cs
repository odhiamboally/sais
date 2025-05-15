using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP.Application.Dtos.Auth;
public record RegisterResponse(string UserId, string Token, string ConfirmationLink);
