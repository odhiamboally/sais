using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP.Domain.Entities;
public class SubLocation
{
    [Key]
    public int Id { get; set; }
    public string? Code { get; set; }
    public string? Name { get; set; }
    public int LocationId { get; set; }
    public Location? Location { get; set; }
    public ICollection<Village>? Villages { get; set; }
}
