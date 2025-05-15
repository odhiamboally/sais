using EssPortal.Domain.Enums.NavEnums;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP.Domain.Entities;
public class Applicant
{
    [Key]
    public int Id { get; set; }
    public DateTimeOffset ApplicationDate { get; set; }
    public string? FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string? LastName { get; set; }
    public int SexId { get; set; }
    public int Age { get; set; }
    public int MaritalStatusId { get; set; }
    public string? IdentificationNumber { get; set; } // ID/Passport (alphanumeric, max 20 chars)
    public int? VillageId { get; set; } // From geo-location hierarchy
    public string? PostalAddress { get; set; }
    public string? PhysicalAddress { get; set; }
    public string? TelephoneContact { get; set; }
    public string? ApplicationStatus { get; set; } // For approval workflow

    // Navigation properties
    public Sex Sex { get; set; }
    public MaritalStatus MaritalStatus { get; set; }
    public Village Village { get; set; }
    public ICollection<ApplicantProgram> ApplicantPrograms { get; set; }
}
