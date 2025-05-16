using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP.Application.Dtos.Application;
public record ApplicantResponse(
    int Id,
    
    string FirstName,
    string MiddleName,
    string LastName,
    string FullName,
    string Sex,
    DateTimeOffset DateOfBirth,
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
    

    List<string> Programs


    );
