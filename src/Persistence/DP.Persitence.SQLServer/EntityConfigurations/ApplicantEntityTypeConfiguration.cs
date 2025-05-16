using DP.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP.Persitence.SQLServer.EntityConfigurations;
internal sealed class ApplicantEntityTypeConfiguration : IEntityTypeConfiguration<Applicant>
{
    public void Configure(EntityTypeBuilder<Applicant> builder)
    {
        builder
            .Property(s => s.FirstName)
            .IsRequired();

        builder
            .Property(s => s.LastName)
        .IsRequired();

        builder
            .Property(s => s.DateOfBirth)
            .IsRequired();

        builder
            .Property(s => s.IDNumber)
            .IsRequired();

        builder
            .Property(s => s.TelephoneContact)
            .IsRequired();

        builder.Property(s => s.SexId).IsRequired();
        builder.HasOne(a => a.Sex)
               .WithMany()
               .HasForeignKey(a => a.SexId)
               .OnDelete(DeleteBehavior.Restrict); 

        builder.Property(s => s.MaritalStatusId).IsRequired();
        builder.HasOne(a => a.MaritalStatus)
               .WithMany()
               .HasForeignKey(a => a.MaritalStatusId)
               .OnDelete(DeleteBehavior.Restrict); 

        builder.Property(s => s.CountyId).IsRequired();
        builder.HasOne(a => a.County)
               .WithMany()
               .HasForeignKey(a => a.CountyId)
               .OnDelete(DeleteBehavior.Restrict); 

        builder.Property(s => s.SubCountyId).IsRequired();
        builder.HasOne(a => a.SubCounty)
               .WithMany()
               .HasForeignKey(a => a.SubCountyId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.Property(s => s.LocationId).IsRequired();
        builder.HasOne(a => a.Location)
               .WithMany()
               .HasForeignKey(a => a.LocationId)
               .OnDelete(DeleteBehavior.Restrict); 

        builder.Property(s => s.SubLocationId).IsRequired();
        builder.HasOne(a => a.SubLocation)
               .WithMany()
               .HasForeignKey(a => a.SubLocationId)
               .OnDelete(DeleteBehavior.Restrict); 

        builder.Property(s => s.VillageId).IsRequired();
        builder.HasOne(a => a.Village)
               .WithMany()
               .HasForeignKey(a => a.VillageId)
               .OnDelete(DeleteBehavior.Restrict); // Avoid cascade

      

    }
}
