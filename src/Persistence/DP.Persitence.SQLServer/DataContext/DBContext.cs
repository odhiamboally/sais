using DP.Domain.Entities;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace DP.Persistence.SQLServer.DataContext;
public class DBContext : IdentityDbContext<AppUser>
{
    public DBContext(DbContextOptions<DBContext> options) : base(options)
    {
    }

    #region Sets

    public DbSet<AppUser>? AppUsers { get; set; }

    #endregion


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DBContext).Assembly);

    }

    public static DBContext Create()
    {
        var optionsBuilder = new DbContextOptionsBuilder<DBContext>();
        optionsBuilder.UseSqlServer("Connection");
        return new DBContext(optionsBuilder.Options);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        using var transaction = await Database.BeginTransactionAsync(cancellationToken);

        try
        {
            var result = await base.SaveChangesAsync(cancellationToken); // Ensure Save Entity first

            await transaction.CommitAsync(cancellationToken);

            return result;
        }
        catch (Exception)
        {

            await transaction.RollbackAsync(cancellationToken);  // Rollback in case of error
            throw;
        }
    }


}
