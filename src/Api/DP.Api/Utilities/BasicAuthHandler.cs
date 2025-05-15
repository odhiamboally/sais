

using DP.Api.Configuration;

using Microsoft.Extensions.Options;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DP.Api.Utilities;
public class BasicAuthHandler : DelegatingHandler
{
    private readonly IOptions<ApiSettings> _settings;
    public BasicAuthHandler(IOptions<ApiSettings> settings)
    {
        _settings = settings;

    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        try
        {
            var settings = _settings.Value;
            var authToken = Convert.ToBase64String(
                Encoding.ASCII.GetBytes($"{settings.Username}:{settings.Password}"));

            request.Headers.Authorization = new AuthenticationHeaderValue("Basic", authToken);
                
            return await base.SendAsync(request, cancellationToken);
        }
        catch (Exception)
        {

            throw;
        }
       
    }
}
