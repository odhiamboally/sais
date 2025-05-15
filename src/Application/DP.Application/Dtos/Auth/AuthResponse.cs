using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP.Application.Dtos.Auth;
public record AuthResponse(
    string Token,
    string Message

);
