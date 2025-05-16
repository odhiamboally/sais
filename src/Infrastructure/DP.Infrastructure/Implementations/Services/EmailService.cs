using DP.Application.Configuration;
using DP.Application.Contracts.Abstractions.IServices;
using DP.Application.Dtos.Auth;
using DP.Application.Dtos.Common;

using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace DP.Infrastructure.Implementations.Services;
internal sealed class EmailService : IEmailService
{
    private readonly EmailSettings _emailSettings;
    private readonly IMemoryCache _cache;

    public EmailService(IOptions<EmailSettings> emailSettings, IMemoryCache cache)
    {
        _emailSettings = emailSettings.Value;
        _cache = cache;

    }

    public async Task<ApiResponse<SendEmailResponse>> SendEmailAsync(SendEmailRequest sendEmailRequest)
    {
        try
        {
            var mailMessage = new MailMessage
            {
                From = new MailAddress(_emailSettings.FromAddress!),
                Subject = sendEmailRequest.Subject,
                Body = sendEmailRequest.Body,
                IsBodyHtml = true
            };

            mailMessage.To.Add(sendEmailRequest.To!);

            using var client = new SmtpClient(_emailSettings.SmtpServer, _emailSettings.Port)
            {
                Credentials = new System.Net.NetworkCredential(_emailSettings.Username, _emailSettings.Password),
                EnableSsl = true
            };

            await client.SendMailAsync(mailMessage);

            return ApiResponse<SendEmailResponse>.Success("", new SendEmailResponse());

        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<string> ApplyTemplateAsync(string templateName, Dictionary<string, string> replacements)
    {
        try
        {
            string template = await GetTemplateAsync(templateName);

            foreach (var replacement in replacements)
            {
                template = template.Replace($"@{replacement.Key}", replacement.Value);
            }

            return template;
        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<string> GetTemplateAsync(string templateName)
    {
        try
        {
            string cacheKey = $"EmailTemplate_{templateName}";
            if (!_cache.TryGetValue(cacheKey, out string? template)) 
            {
                string templatesPath = _emailSettings.EmailTemplate!; 

                string templatePath = Path.Combine(templatesPath, $"{templateName}.html");

                if (!File.Exists(templatePath))
                {
                    throw new FileNotFoundException($"Email template '{templateName}' not found");
                }

                template = await File.ReadAllTextAsync(templatePath);

                _cache.Set(cacheKey, template, TimeSpan.FromMinutes(60));
            }

            return template!;
        }
        catch (Exception)
        {
            throw;
        }
    }

    
}
