using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lms.Application.Utilities;


public static class ServiceRegistrationCache
{
    public static string? RegistrationToken { get; private set; }

    public static void SetToken(string token)
    {
        RegistrationToken = token;
    }

    public static bool IsRegistered => !string.IsNullOrEmpty(RegistrationToken);
}
