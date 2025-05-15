using Microsoft.IdentityModel.Tokens;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP.Application.Configuration;
public class JwtSettings
{
    public string? Secret { get; set; }
    public string? Issuer { get; set; }
    public string? Audience { get; set; }
    public int ExpirationInMinutes { get; set; }
    public int ClockSkew { get; set; }
    public string? JwtSecurityKey { get; set; }
    public SymmetricSecurityKey? SignKey { get; set; }
}
