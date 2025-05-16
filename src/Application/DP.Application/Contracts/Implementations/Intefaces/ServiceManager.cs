using DP.Application.Contracts.Abstractions.Interfaces;
using DP.Application.Contracts.Abstractions.IServices;


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP.Application.Contracts.Implementations.Intefaces;
internal sealed class ServiceManager : IServiceManager
{
    public IApplicantService ApplicantService { get; }
    public IApplicationService ApplicationService { get; }
    public IEmailService EmailService { get; }
    

    public ServiceManager(
        IApplicantService applicantService,
        IApplicationService applicationService,
        IEmailService emailService
        

        )
    {
        ApplicantService = applicantService ?? throw new ArgumentNullException(nameof(applicantService));
        ApplicationService = applicationService ?? throw new ArgumentNullException(nameof(applicationService));
        EmailService = emailService ?? throw new ArgumentNullException(nameof(emailService));

    }
}
