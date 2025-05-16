using DP.Application.Configuration;
using DP.Application.Dtos.Application;
using DP.Application.Dtos.Common;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP.Application.Contracts.Abstractions.IServices;
public interface IApplicantService
{
    Task<ApiResponse<ApplicantResponse>> CreateAsync(CreateApplicantRequest createApplicantRequest);
    Task<ApiResponse<ApplicantResponse>> DeleteAsync(int Id);
    Task<ApiResponse<ApplicantResponse>> GetAllAsync(PaginationSettings paginationSetting);
    Task<ApiResponse<ApplicantResponse>> GetByIdAsync(int Id);
    Task<ApiResponse<ApplicantResponse>> UpdateAsync(UpdateApplicantRequest updateApplicantRequest);
    
}
