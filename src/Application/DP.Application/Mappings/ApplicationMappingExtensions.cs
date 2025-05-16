using DP.Application.Dtos.Application;
using DP.Domain.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP.Application.Mappings;
public static class ApplicationMappingExtensions
{
    public static ApplicationResponse ToResponse(this Domain.Entities.Application entity)
    {
        return new ApplicationResponse(
            entity.Id,
            entity.ApplicantId,
            entity.Applicant?.FirstName ?? string.Empty,
            entity.Applicant?.MiddleName ?? string.Empty,
            entity.Applicant?.LastName ?? string.Empty,
            entity.ProgrammeId,
            entity.Program?.Name ?? string.Empty,
            entity.ApplicationDate,
            entity.DeclarationAccepted,
            entity.DeclarationDate,
            entity.Status
        );
    }

    public static Domain.Entities.Application ToEntity(this CreateApplicationRequest dto)
    {
        return new Domain.Entities.Application
        {
            ApplicantId = dto.ApplicantId,
            ProgrammeId = dto.ProgrammeId,
            ApplicationDate = dto.ApplicationDate,
            DeclarationAccepted = dto.DeclarationAccepted,
            DeclarationDate = dto.DeclarationDate,
            Status = dto.ApplicationStatus

        };
    }

    public static void MapFrom(this Domain.Entities.Application entity, UpdateApplicationRequest dto)
    {
        entity.ApplicantId = dto.ApplicantId;
        entity.ProgrammeId = dto.ProgrammeId;
        entity.ApplicationDate = dto.ApplicationDate;
        entity.DeclarationAccepted = dto.DeclarationAccepted;
        entity.DeclarationDate = dto.DeclarationDate;
        entity.Status = dto.ApplicationStatus;
    }
}
