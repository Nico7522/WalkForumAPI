

using WalkForum.Domain.EmailModel;

namespace WalkForum.Domain.Repositories;

public interface IEmailRepository
{
    Task Send(EmailMetadata emailMetadata);
    Task SendUsingTemplate(EmailMetadata emailMetadata, string username, string templateName);
}
