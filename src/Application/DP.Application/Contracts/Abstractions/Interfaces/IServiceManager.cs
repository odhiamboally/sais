using DP.Application.Contracts.Abstractions.IServices;


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP.Application.Contracts.Abstractions.Interfaces;
public interface IServiceManager
{
    IIdentityService AuthService { get; }
    IEmailService EmailService { get; }
    

}
