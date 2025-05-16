using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP.Application.Dtos.Application;
public record UpdateApplicationRequest(
    int Id,
    int ApplicantId,
    int ProgrammeId,
    DateTimeOffset ApplicationDate,
    bool DeclarationAccepted,
    DateTimeOffset? DeclarationDate,
    string? ApplicationStatus
    );