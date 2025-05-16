using DP.Application.Configuration;
using DP.Application.Contracts.Abstractions.Caching;
using DP.Application.Contracts.Abstractions.Interfaces;
using DP.Application.Contracts.Abstractions.IServices;
using DP.Application.Contracts.Implementations.Caching;
using DP.Application.Contracts.Implementations.Intefaces;
using DP.Application.Contracts.Implementations.Services;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using StackExchange.Redis;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP.Application.Utilities;
public static  class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        try
        {
            var jsonSettings = new JsonSettings();
            configuration.GetSection("JsonSettings").Bind(jsonSettings);
            services.AddSingleton(jsonSettings);

            var cacheSettings = new CacheSettings();
            configuration.GetSection("CacheSettings").Bind(cacheSettings);
            services.AddSingleton(cacheSettings);
            CacheKeyGenerator.Configure(cacheSettings);

            services.AddScoped<IServiceManager, ServiceManager>();
            
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IClaimsService, ClaimsService>();

            services.AddScoped<IApplicantService, ApplicantService>();
            services.AddScoped<IApplicationService, ApplicationService>();

            switch (cacheSettings.CacheType)
            {
                case string type when type.Equals("redis", StringComparison.OrdinalIgnoreCase):

                    services.AddSingleton<IConnectionMultiplexer>(sp => ConnectionMultiplexer.Connect(cacheSettings.Redis!.Configuration!));

                    services.AddStackExchangeRedisCache(options =>
                    {
                        options.Configuration = cacheSettings.Redis!.Configuration;
                        options.InstanceName = cacheSettings.Redis.InstanceName;
                    });
                    services.AddSingleton<ICacheService, RedisMultiplexerCacheService>();
                    services.AddSingleton<ICacheService, RedisCacheService>();
                    break;

                case string type when type.Equals("azure", StringComparison.OrdinalIgnoreCase):
                    services.AddStackExchangeRedisCache(options =>
                    {
                        options.Configuration = cacheSettings.Azure!.ConnectionString;
                    });
                    services.AddSingleton<ICacheService, AzureCacheService>();
                    break;

                case string type when type.Equals("aws", StringComparison.OrdinalIgnoreCase):
                    services.AddStackExchangeRedisCache(options =>
                    {
                        options.Configuration = cacheSettings.Aws!.Endpoint;
                    });
                    services.AddSingleton<ICacheService, ElastiCacheService>();
                    break;

                default:
                    services.AddMemoryCache();
                    services.AddSingleton<ICacheService, InMemoryCacheService>();
                    break;
            }



            return services;
        }
        catch (Exception)
        {

            throw;
        }
        
    }
}
