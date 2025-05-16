using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP.Application.Dtos.Application;
public record CreateApplicantRequest(
    
    DateTimeOffset ApplicationDate,
    string? FirstName,
    string? MiddleName,
    string? LastName,
    DateTimeOffset DateOfBirth,
    int Age,
    string? IDNumber,
    string? PostalAddress,
    string? PhysicalAddress,
    string? TelephoneContact,
    int SexId,
    int MaritalStatusId,
    int CountyId,
    int SubCountyId,
    int LocationId,
    int SubLocationId,
    int VillageId,
    int ProgrammeId,
    bool DeclarationAccepted,
    DateTimeOffset? DeclarationDate,
    string? NameSignature,
    byte[]? ImageSignature,
    bool IsDeleted,

    List<int>? ProgramIds

);
