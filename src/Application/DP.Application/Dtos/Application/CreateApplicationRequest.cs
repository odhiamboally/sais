using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP.Application.Dtos.Application;
public record CreateApplicationRequest(
    int ApplicantId,
    int ProgrammeId,
    DateTime ApplicationDate,
    bool DeclarationAccepted,
    DateTime? DeclarationDate,
    string? ApplicationStatus
    );