namespace ClickNPick.Application.Abstractions.Services;

public interface IEmailSender
{
    public Task SendEmailAsync(string recipientEmail, string subject, string body);
}
