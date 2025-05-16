using DP.Domain.Entities;
using DP.Domain.Interfaces;
using DP.Domain.IRepositories;
using DP.Infrastructure.Implementations.Repositories;
using DP.Persistence.SQLServer.DataContext;
using DP.Persistence.SQLServer.Implementations.Intefaces;
using DP.Persistence.SQLServer.Implementations.Repositories;
using DP.Persitence.SQLServer.Implementations.Repositories;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using Polly;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace DP.Persistence.SQLServer.Utilities;
public static class DependencyInjection
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        try
        {

            var ConnString = configuration.GetConnectionString("Connection");
            services.AddDbContext<DBContext>(options => options.UseSqlServer(ConnString!));

            // Add Identity with AppUser
            services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<DBContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IApplicantRepository, ApplicantRepository>();
            services.AddScoped<IApplicationRepository, ApplicationRepository>();

            return services;

        }
        catch (Exception)
        {
            throw;
        }
    }
}
