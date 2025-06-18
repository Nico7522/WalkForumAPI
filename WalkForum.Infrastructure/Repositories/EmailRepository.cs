
using FluentEmail.Core;
using Microsoft.AspNetCore.Identity;
using System.Net.Mail;
using WalkForum.Domain.EmailModel;
using WalkForum.Domain.Entities;
using WalkForum.Domain.Exceptions;
using WalkForum.Domain.Repositories;

namespace WalkForum.Infrastructure.Repositories;

internal class EmailRepository(IFluentEmail fluentEmail) : IEmailRepository
{
    public async Task Send(EmailMetadata emailMetadata)
    {
        try
        {
            await fluentEmail.To(emailMetadata.ToAddress)
                .Subject(emailMetadata.Subject)
                .Body(emailMetadata.Body)
                .SendAsync();

        }
        catch (SmtpException ex)
        {
                Console.WriteLine(ex.Message);
        }
    }

    public async Task SendUsingTemplate(EmailMetadata emailMetadata, string username, string templateName)
    {
      
        await fluentEmail.To(emailMetadata.ToAddress)
             .Subject(emailMetadata.Subject)
             .UsingTemplateFromFile(templateName, username)
             .SendAsync();
    }
}
