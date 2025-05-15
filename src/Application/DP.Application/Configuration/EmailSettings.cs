using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP.Application.Configuration;
public class EmailSettings
{
    public string? SmtpServer { get; set; }
    public int Port { get; set; }
    public string? Username { get; set; }
    public string? DisplayName { get; set; }
    public string? Password { get; set; }
    public string? FromAddress { get; set; }
    public string? EmailTemplate { get; set; }
    public string? ClientBaseUrl { get; set; }
}
