using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP.Application.Dtos.Application;
public record ApplicationResponse(
    int Id,
    int ApplicantId,
    string ApplicantFirstName,
    string ApplicantMiddleName,
    string ApplicantLastName,
    int ProgrammeId,
    string ProgrammeName,
    DateTimeOffset ApplicationDate,
    bool DeclarationAccepted,
    DateTimeOffset? DeclarationDate,
    string? ApplicationStatus
    );
