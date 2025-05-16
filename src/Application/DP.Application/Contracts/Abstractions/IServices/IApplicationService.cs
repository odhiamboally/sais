using DP.Application.Configuration;
using DP.Application.Dtos.Application;
using DP.Application.Dtos.Common;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP.Application.Contracts.Abstractions.IServices;
public interface IApplicationService
{
    Task<ApiResponse<ApplicationResponse>> CreateAsync(CreateApplicationRequest createApplicationRequest);
    Task<ApiResponse<ApplicationResponse>> DeleteAsync(int Id);
    Task<ApiResponse<ApplicationResponse>> GetAllAsync(PaginationSettings paginationSetting);
    Task<ApiResponse<ApplicationResponse>> GetByIdAsync(int Id);
    Task<ApiResponse<ApplicationResponse>> UpdateAsync(UpdateApplicantRequest updateApplicationRequest);
}
