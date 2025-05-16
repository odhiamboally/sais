using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DP.Persistence.SQLServer.DataContext;
using Microsoft.Extensions.Configuration;

namespace DP.Persitence.SQLServer.Utilities;

public class DBContextFactory : IDesignTimeDbContextFactory<DBContext>
{
    public DBContext CreateDbContext(string[] args)
    {
        // Build configuration from appsettings.json
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory()) // Path to the project root
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true) // Load appsettings.json
            .Build();

        // Retrieve the connection string
        var connString = configuration.GetConnectionString("Connection");

        var optionsBuilder = new DbContextOptionsBuilder<DBContext>();

        optionsBuilder.UseSqlServer(connString);

        return new DBContext(optionsBuilder.Options);
    }
}
