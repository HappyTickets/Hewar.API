using System.Net.Mail;

namespace Application.Common.Interfaces.Services;

public interface IEmailSender
{
    Task<Result<Empty>> SendEmailAsync(string recipient, string subject, string message, IEnumerable<Attachment>? attachments = null, CancellationToken cancellationToken = default);
    Result<Empty> SendEmail(string recipient, string subject, string message, IEnumerable<Attachment>? attachments = null);
}