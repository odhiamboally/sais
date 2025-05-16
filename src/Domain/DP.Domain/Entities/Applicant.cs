
using Microsoft.AspNetCore.Identity;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP.Domain.Entities;
public class Applicant
{
    
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string? LastName { get; set; }
    public DateTimeOffset DateOfBirth { get; set; }
    public int Age { get; set; }

    [Key]
    public string? IDNumber { get; set; }
    public string? PostalAddress { get; set; }
    public string? PhysicalAddress { get; set; }
    public string? TelephoneContact { get; set; }
    public int SexId { get; set; }
    public int MaritalStatusId { get; set; }
    public int CountyId { get; set; }
    public int SubCountyId { get; set; }
    public int LocationId { get; set; }
    public int SubLocationId { get; set; }
    public int VillageId { get; set; }

    
    public string? NameSignature { get; set; }
    public byte[]? ImageSignature { get; set; }
    public bool IsDeleted { get; set; }

    public Sex? Sex { get; set; }
    public MaritalStatus? MaritalStatus { get; set; }
    public County? County { get; set; }
    public SubCounty? SubCounty { get; set; }
    public Location? Location { get; set; }
    public SubLocation? SubLocation { get; set; }
    public Village? Village { get; set; }
    



}
