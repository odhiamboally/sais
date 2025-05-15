using DP.Application.Contracts.Abstractions.IServices;
using DP.Domain.IRepositories;
using DP.Infrastructure.Implementations.Services;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using Polly;

using System.Net.Http.Headers;
using System.Text;

namespace DP.Infrastructure.Utilities;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
		try
		{
            services.AddScoped<IIdentityService, IdentityService>();


            return services;

        }
		catch (Exception)
		{

			throw;
		}
        

       
    }
}
