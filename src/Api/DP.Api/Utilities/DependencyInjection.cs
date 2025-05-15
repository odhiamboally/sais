using AspNetCoreRateLimit;

using DP.Api.Configuration;
using DP.Application.Configuration;

using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Extensions.Logging;
using Serilog.Formatting.Compact;
using Serilog.Formatting.Json;
using System.Net;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json;
using DP.Domain.Entities;
using DP.Persistence.SQLServer.DataContext;
using Microsoft.EntityFrameworkCore;
using Asp.Versioning;
using DP.Api.Middleware;
using Microsoft.AspNetCore.Mvc;

namespace DP.Api.Utilities;

public static class DependencyInjection
{
    private static IHttpContextAccessor httpContextAccessor = new HttpContextAccessor();

    public class CorrelationIdEnricher : ILogEventEnricher
    {
        private const string CorrelationIdPropertyName = "CorrelationId";

        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            var correlationId = GetCorrelationId(); 
            var correlationIdProperty = new LogEventProperty(CorrelationIdPropertyName, new ScalarValue(correlationId));

            logEvent.AddOrUpdateProperty(correlationIdProperty);
        }

        private string GetCorrelationId()
        {
            // Retrieve the correlation ID from the HTTP context if available
            var httpContext = httpContextAccessor.HttpContext;
            var correlationId = httpContext?.Request.Headers["CorrelationId"].FirstOrDefault();

            if (string.IsNullOrEmpty(correlationId))
            {
                correlationId = Guid.NewGuid().ToString();
            }

            return correlationId;
        }
    }

    public class IPAddressEnricher : ILogEventEnricher
    {
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            var httpContext = httpContextAccessor.HttpContext;
            var remoteIpAddress = httpContext?.Connection?.RemoteIpAddress;

            // Check if the IP address is available and not in IPv6 loopback format
            if (remoteIpAddress != null && !IPAddress.IsLoopback(remoteIpAddress))
            {
                var ipAddress = remoteIpAddress.ToString();
                var ipAddressProperty = propertyFactory.CreateProperty("IPAddress", ipAddress);
                logEvent.AddPropertyIfAbsent(ipAddressProperty);
            }
        }
    }
   
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        try
        {
            var assembly = typeof(Program).Assembly;

            services.Configure<JsonSettings>(configuration.GetSection("JsonSettings"));

            services.AddExceptionHandler<ApiExceptionHandler>();
            services.AddProblemDetails();

            services.AddRouting(options => options.LowercaseUrls = true);

            services.AddWebEncoders();
            services.AddMvc(options =>
            {
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            });

            services.AddHttpClient();
            services.AddHttpContextAccessor();

            services.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.ReportApiVersions = true;
                config.ApiVersionReader = new UrlSegmentApiVersionReader();
            });


            var serviceMappings = configuration.GetSection("ServiceMappings").Get<Dictionary<string, string>>();

            services.AddValidatorsFromAssembly(assembly);


            return services;
        }
        catch (Exception)
        {

            throw;
        }
        
    }

    public static IServiceCollection AddAuthenticationServices(this IServiceCollection services, IConfiguration configuration)
    {
        try
        {
            services.AddTransient<BasicAuthHandler>();

            var jwtSettings = new JwtSettings();
            configuration.GetSection("JwtSettings").Bind(jwtSettings);

            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

            services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<DBContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.ClaimsIdentity.UserNameClaimType = "Username";

                // User settings
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";

                options.User.RequireUniqueEmail = true;

                // Password settings
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredLength = 8;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // Cookie settings
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;
                options.SignIn.RequireConfirmedAccount = false;
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = jwtSettings?.Issuer,
                    ValidAudience = jwtSettings?.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings?.JwtSecurityKey!)),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    RequireExpirationTime = true,
                    LifetimeValidator = (notBefore, expires, token, parameters) =>
                    {
                        return expires > DateTime.Now;
                    }
                };
                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception is SecurityTokenExpiredException)
                        {
                            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                            context.Response.ContentType = "application/json";

                            var expirationTime = context.Exception is SecurityTokenExpiredException expiredException
                                ? expiredException.Expires.ToString("yyyy-MM-ddTHH:mm:ss")
                                : string.Empty;

                            var errorMessage = new
                            {
                                context.Response.StatusCode,
                                Message = "Token has expired.",
                                ExpirationTime = expirationTime
                            };

                            return context.Response.WriteAsync(JsonSerializer.Serialize(errorMessage));
                        }

                        return Task.CompletedTask;
                    }
                };
            });

            
            return services;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public static IServiceCollection AddLoggingServices(this IServiceCollection services, IConfiguration configuration)
    {
        try
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .MinimumLevel.Information()
                .MinimumLevel.Override("System", LogEventLevel.Warning)
                .MinimumLevel.Override("Serilog", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .Enrich.With(new CorrelationIdEnricher())
                .Enrich.WithProperty("MachineName", Environment.MachineName)
                .Enrich.With<IPAddressEnricher>()
                .WriteTo.Async(s => s.Console(new CompactJsonFormatter()))
                .WriteTo.Async(s => s.File(
                    path: configuration["Serilog:WriteTo:1:Args:path"]!, // Read path from appsettings.json
                    rollingInterval: RollingInterval.Day,
                    formatter: new JsonFormatter())) // Ensure JSON formatting
                .CreateLogger();

            services.AddSingleton<ILoggerFactory>(provider =>
            {
                return new SerilogLoggerFactory(Log.Logger, true);
            });

            return services;
        }
        catch (Exception)
        {

            throw;
        }

    }

    public static IServiceCollection AddRateLimitingServices(this IServiceCollection services)
    {
        try
        {
            // Add IP Rate Limiting
            services.Configure<IpRateLimitOptions>(options =>
            {
                options.EnableEndpointRateLimiting = true;
                options.StackBlockedRequests = false;
                options.HttpStatusCode = 429;
                options.GeneralRules = new List<RateLimitRule>  // Changed from RateLimit.Rules.RateLimitRule
                {
                    new RateLimitRule
                    {
                        Endpoint = "*",
                        Period = "1s",
                        Limit = 10
                    }
                };
            });

            services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
            services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
            services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();

            return services;
        }
        catch (Exception)
        {

            throw;
        }
        
    }

    public static IApplicationBuilder UseSecurityHeaders(this IApplicationBuilder app)
    {
        try
        {
            return app.Use(async (context, next) =>
            {
                context.Response.Headers.Append("X-Content-Type-Options", "nosniff");
                context.Response.Headers.Append("X-Frame-Options", "DENY");
                context.Response.Headers.Append("X-XSS-Protection", "1; mode=block");
                context.Response.Headers.Append("Referrer-Policy", "strict-origin-when-cross-origin");

                await next();
            });
        }
        catch (Exception)
        {

            throw;
        }
        
    }


}
