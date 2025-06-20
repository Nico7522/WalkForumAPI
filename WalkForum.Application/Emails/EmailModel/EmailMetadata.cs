namespace WalkForum.Application.Emails.EmailModel;

public class EmailMetadata(string toAdress, string subject, string? body = null, string? attachmentPath = null)
{
    public string ToAddress { get; set; } = toAdress;
    public string Subject { get; set; } = subject;
    public string? Body { get; set; } = body;
    public string? AttachmentPath { get; set; } = attachmentPath;
}
