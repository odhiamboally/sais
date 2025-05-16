using DP.Application.Dtos.Application;
using DP.Domain.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP.Application.Mappings;
public static class ApplicantMappingExtensions
{
    public static ApplicantResponse ToResponse(this Applicant entity)
    {
        return new ApplicantResponse(
            entity.Id,
            entity.FirstName ?? string.Empty,
            entity.MiddleName ?? string.Empty,
            entity.LastName ?? string.Empty,
            entity.FirstName + " " + entity.MiddleName + " " + entity.LastName,
            entity.Sex?.Name ?? string.Empty,
            entity.DateOfBirth,
            entity.Age,
            entity.MaritalStatus?.Name ?? string.Empty,
            entity.IDNumber ?? string.Empty,
            entity.County?.Name ?? string.Empty,
            entity.SubCounty?.Name ?? string.Empty,
            entity.Location?.Name ?? string.Empty,
            entity.SubLocation?.Name ?? string.Empty,
            entity.Village?.Name ?? string.Empty,
            entity.PostalAddress ?? string.Empty,
            entity.PhysicalAddress ?? string.Empty,
            entity.TelephoneContact ?? string.Empty,
            []


        );
    }

    public static Applicant ToEntity(this CreateApplicantRequest dto)
    {
        return new Applicant
        {
            FirstName = dto.FirstName,
            MiddleName = dto.MiddleName,
            LastName = dto.LastName,
            DateOfBirth = dto.DateOfBirth,
            IDNumber = dto.IDNumber,
            PostalAddress = dto.PostalAddress,
            PhysicalAddress = dto.PhysicalAddress,
            TelephoneContact = dto.TelephoneContact,
            SexId = dto.SexId,
            MaritalStatusId = dto.MaritalStatusId,
            CountyId = dto.CountyId,
            SubCountyId = dto.SubCountyId,
            LocationId = dto.LocationId,
            SubLocationId = dto.SubLocationId,
            VillageId = dto.VillageId,
            NameSignature = dto.NameSignature,
            ImageSignature = dto.ImageSignature,
            IsDeleted = dto.IsDeleted,
        };
    }

    public static void MapFrom(this Applicant entity, UpdateApplicantRequest dto)
    {
        entity.FirstName = dto.FirstName;
        entity.MiddleName = dto.MiddleName;
        entity.LastName = dto.LastName;
        entity.DateOfBirth = dto.DateOfBirth;
        entity.IDNumber = dto.IDNumber;
        entity.PostalAddress = dto.PostalAddress;
        entity.PhysicalAddress = dto.PhysicalAddress;
        entity.TelephoneContact = dto.TelephoneContact;
        entity.SexId = dto.SexId;
        entity.MaritalStatusId = dto.MaritalStatusId;
        entity.CountyId = dto.CountyId;
        entity.SubCountyId = dto.SubCountyId;
        entity.LocationId = dto.LocationId;
        entity.SubLocationId = dto.SubLocationId;
        entity.VillageId = dto.VillageId;
        entity.NameSignature = dto.NameSignature;
        entity.ImageSignature = dto.ImageSignature;
        entity.IsDeleted = dto.IsDeleted;

    }
}
