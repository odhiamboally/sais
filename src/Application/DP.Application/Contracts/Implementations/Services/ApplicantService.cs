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
internal sealed class ApplicantService : IApplicantService
{
    private readonly IUnitOfWork _unitOfWork;
    public ApplicantService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    }

    public Task<ApiResponse<ApplicantResponse>> CreateAsync(CreateApplicantRequest createApplicantRequest)
    {
        throw new NotImplementedException();
    }

    public Task<ApiResponse<ApplicantResponse>> DeleteAsync(int Id)
    {
        throw new NotImplementedException();
    }

    public Task<ApiResponse<ApplicantResponse>> GetAllAsync(PaginationSettings paginationSetting)
    {
        throw new NotImplementedException();
    }

    public Task<ApiResponse<ApplicantResponse>> GetByIdAsync(int Id)
    {
        throw new NotImplementedException();
    }

    public Task<ApiResponse<ApplicantResponse>> UpdateAsync(UpdateApplicantRequest updateApplicantRequest)
    {
        throw new NotImplementedException();
    }
}
