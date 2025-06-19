
using FluentEmail.Core;
using System.Net.Mail;
using WalkForum.Domain.EmailModel;

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

    public async Task SendUsingTemplate<T>(EmailMetadata emailMetadata, T data, string templateName)
    {
        var templatePath = Path.Combine(AppContext.BaseDirectory, "EmailTemplates", templateName);

        if (!File.Exists(templatePath))
        {
            throw new FileNotFoundException($"Template file '{templatePath}' not found.");
        }
        await fluentEmail.To(emailMetadata.ToAddress)
             .Subject(emailMetadata.Subject)
             .UsingTemplateFromFile(templatePath, data)
             .SendAsync();
    }
}
