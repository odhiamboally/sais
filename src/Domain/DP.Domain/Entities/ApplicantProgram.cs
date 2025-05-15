using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP.Domain.Entities;
public class ApplicantProgram
{
    [Key]
    public int Id { get; set; }
    public int ApplicantId { get; set; }
    public int ProgramId { get; set; }

    public Applicant? Applicant { get; set; }
    public Program? Program { get; set; }
}
