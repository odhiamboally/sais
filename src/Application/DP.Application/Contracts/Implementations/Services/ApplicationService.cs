using DP.Application.Configuration;
using DP.Application.Contracts.Abstractions.IServices;
using DP.Application.Dtos.Application;
using DP.Application.Dtos.Common;
using DP.Domain.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP.Application.Contracts.Implementations.Services;
internal sealed class ApplicationService : IApplicationService
{
    private readonly IUnitOfWork _unitOfWork;
    public ApplicationService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    }

    public Task<ApiResponse<ApplicationResponse>> CreateAsync(CreateApplicationRequest createApplicationRequest)
    {
        throw new NotImplementedException();
    }

    public Task<ApiResponse<ApplicationResponse>> DeleteAsync(int Id)
    {
        throw new NotImplementedException();
    }

    public Task<ApiResponse<ApplicationResponse>> GetAllAsync(PaginationSettings paginationSetting)
    {
        throw new NotImplementedException();
    }

    public Task<ApiResponse<ApplicationResponse>> GetByIdAsync(int Id)
    {
        throw new NotImplementedException();
    }

    public Task<ApiResponse<ApplicationResponse>> UpdateAsync(UpdateApplicantRequest updateApplicationRequest)
    {
        throw new NotImplementedException();
    }
}
