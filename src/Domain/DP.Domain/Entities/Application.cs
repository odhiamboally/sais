using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP.Domain.Entities;
public class Application
{
    [Key]
    public int Id { get; set; }
    public int ApplicantId { get; set; }
    public int ProgrammeId { get; set; }
    public DateTimeOffset ApplicationDate { get; set; }
    public bool DeclarationAccepted { get; set; }
    public DateTimeOffset? DeclarationDate { get; set; }
    public string? Status { get; set; }

    public Applicant? Applicant { get; set; }
    public Programme? Program { get; set; }
}
