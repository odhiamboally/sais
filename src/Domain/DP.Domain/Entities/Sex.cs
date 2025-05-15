using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP.Domain.Entities;
public class Sex
{
    [Key]
    public int Id { get; set; }
    public string? Name { get; set; }
    public ICollection<Applicant>? Applicants { get; set; }
}
