

using WalkForum.Domain.EmailModel;

namespace WalkForum.Domain.Repositories;

public interface IEmailRepository
{
    Task Send(EmailMetadata emailMetadata);
    Task SendUsingTemplate<T>(EmailMetadata emailMetadata, T data, string templateName);
}
