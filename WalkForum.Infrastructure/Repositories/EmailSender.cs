using Microsoft.Extensions.Logging;
using WalkForum.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

sealed class EmailSender : IEmailSender<User>
{
    private readonly ILogger _logger;

    public EmailSender(ILogger<EmailSender> logger)
    {
        _logger = logger;
    }
    public List<Email> Emails { get; set; } = new();

    public Task SendConfirmationLinkAsync(User user, string email, string confirmationLink)
    {
        throw new NotImplementedException();
    }

    public Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        _logger.LogWarning($"{email} {subject} {htmlMessage}");
        Emails.Add(new(email, subject, htmlMessage));
        return Task.CompletedTask;
    }

    public Task SendPasswordResetCodeAsync(User user, string email, string resetCode)
    {
        throw new NotImplementedException();
    }

    public Task SendPasswordResetLinkAsync(User user, string email, string resetLink)
    {
        throw new NotImplementedException();
    }
}
sealed record Email(string Address, string Subject, string HtmlMessage);