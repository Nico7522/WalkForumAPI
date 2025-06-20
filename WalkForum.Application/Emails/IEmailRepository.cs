using WalkForum.Application.Emails.EmailModel;

namespace WalkForum.Application.Emails;

public interface IEmailRepository
{
    Task Send(EmailMetadata emailMetadata);
    Task SendUsingTemplate<T>(EmailMetadata emailMetadata, T data, string templateName);
}
