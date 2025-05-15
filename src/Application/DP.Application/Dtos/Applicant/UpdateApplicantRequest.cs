using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP.Application.Dtos.Applicant;
public record UpdateApplicantRequest
{
    public int Id { get; set; }
    public DateTime ApplicationDate { get; init; }
    public string? FirstName { get; init; }
    public string? MiddleName { get; init; }
    public string? LastName { get; init; }
    public int SexId { get; init; }
    public int Age { get; init; }
    public int MaritalStatusId { get; init; }
    public string? IDNumber { get; init; }
    public int? VillageId { get; init; }
    public string? PostalAddress { get; init; }
    public string? PhysicalAddress { get; init; }
    public string? TelephoneContact { get; init; }
    public List<int>? ProgramIds { get; init; }
    public string? ApplicationStatus { get; set; }
}
