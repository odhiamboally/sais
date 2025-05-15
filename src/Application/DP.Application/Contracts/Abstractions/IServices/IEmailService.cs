using DP.Application.Dtos.Auth;
using DP.Application.Dtos.Common;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace DP.Application.Contracts.Abstractions.IServices;
public interface IEmailService
{
    Task<string> GetTemplateAsync(string templateName);
    Task<string> ApplyTemplateAsync(string templateName, Dictionary<string, string> replacements);
    Task<ApiResponse<SendEmailResponse>> SendEmailAsync(SendEmailRequest sendEmailRequest);
}
