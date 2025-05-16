using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP.Application.Dtos.Application;
public record ApplicantFilter
{
    public string? Name { get; set; }
    public DateTimeOffset? FromDate { get; set; }
    public DateTimeOffset? ToDate { get; set; }
    public string? Status { get; set; }
    public int? CountyId { get; set; }
    public int? SubCountyId { get; set; }
    public int? LocationId { get; set; }
    public int? SubLocationId { get; set; }
    public int? VillageId { get; set; }
    
}
