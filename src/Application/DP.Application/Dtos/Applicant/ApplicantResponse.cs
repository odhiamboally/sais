using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP.Application.Dtos.Applicant;
public record ApplicantResponse(
    int Id,
    DateTimeOffset ApplicationDate,
    string FullName, 
    string Sex,
    int Age,
    string MaritalStatus,
    string IdentificationNumber,
    string County,
    string SubCounty,
    string Location,
    string SubLocation,
    string Village,
    string PostalAddress,
    string PhysicalAddress,
    string TelephoneContact,
    string ApplicationStatus,
    List<string> Programs
);
