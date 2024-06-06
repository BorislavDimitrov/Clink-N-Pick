using ClickNPick.Application.Abstractions.Services;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;

namespace ClickNPick.Infrastructure.Services;

public class EmailSender : IEmailSender
{
    private readonly string SmtpServer;
    private readonly int Port;
    private readonly bool UseSsl;
    private readonly string SenderEmail;
    private readonly string AppPassword;

    public EmailSender(IConfiguration configuration)
    {
        SmtpServer = configuration.GetValue<string>("EmailSender:SmtpServer");
        Port = configuration.GetValue<int>("EmailSender:Port");
        UseSsl = configuration.GetValue<bool>("EmailSender:UseSsl");
        SenderEmail = configuration.GetValue<string>("EmailSender:SenderEmail");
        AppPassword = configuration.GetValue<string>("EmailSender:AppPassword"); 
    }

    public async Task SendEmailAsync(string recipientEmail, string subject, string body)
    {
        using (var mailMessage = new MailMessage())
        {
            mailMessage.From = new MailAddress(SenderEmail);
            mailMessage.To.Add(new MailAddress(recipientEmail));
            mailMessage.Subject = subject;
            mailMessage.Body = body;

            mailMessage.IsBodyHtml = true;

            using (var smtpClient = new SmtpClient(SmtpServer, Port))
            {
                smtpClient.EnableSsl = UseSsl;
                smtpClient.Credentials = new NetworkCredential(SenderEmail, AppPassword);
                await smtpClient.SendMailAsync(mailMessage);
            }
        }
    }
}
