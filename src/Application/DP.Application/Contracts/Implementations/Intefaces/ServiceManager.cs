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
    public IIdentityService AuthService { get; }
    public IEmailService EmailService { get; }
    

    public ServiceManager(
        IIdentityService accountService,
        IEmailService emailService
        

        )
    {
        AuthService = accountService;
        EmailService = emailService;

    }
}
