using System.Net;
using System.Text;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using JuniorLeagueCup.WebSite.Options;

namespace JuniorLeagueCup.WebSite.Services;

public interface IEmailService
{
    Task<bool> SendFormEmailAsync(string subject, IEnumerable<(string Label, string? Value)> fields, string? replyToEmail = null, string? replyToName = null);
}

public class EmailService(IOptions<EmailSettings> options, ILogger<EmailService> logger) : IEmailService
{
    public async Task<bool> SendFormEmailAsync(
        string subject,
        IEnumerable<(string Label, string? Value)> fields,
        string? replyToEmail = null,
        string? replyToName = null)
    {
        var settings = options.Value;
        if (string.IsNullOrWhiteSpace(settings.Password))
        {
            logger.LogError("E-posta gönderilemedi: SMTP şifresi yapılandırılmamış.");
            return false;
        }

        try
        {
            var message = BuildMessage(settings, subject, fields, replyToEmail, replyToName);
            using var client = CreateClient();
            await ConnectAsync(client, settings);
            await client.AuthenticateAsync(settings.Username, settings.Password);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);

            logger.LogInformation("Form e-postası gönderildi: {Subject} -> {To}", subject, settings.ToAddress);
            return true;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Form e-postası gönderilemedi: {Subject}", subject);
            return false;
        }
    }

    private static MimeMessage BuildMessage(
        EmailSettings settings,
        string subject,
        IEnumerable<(string Label, string? Value)> fields,
        string? replyToEmail,
        string? replyToName)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress(settings.FromName, settings.FromAddress));
        message.To.Add(MailboxAddress.Parse(settings.ToAddress));
        message.Subject = subject;

        if (!string.IsNullOrWhiteSpace(replyToEmail))
            message.ReplyTo.Add(new MailboxAddress(replyToName ?? replyToEmail, replyToEmail));

        var body = new StringBuilder();
        body.AppendLine("<html><body style=\"font-family:Arial,sans-serif;line-height:1.5;color:#222;\">");
        body.AppendLine($"<h2 style=\"color:#c41e3a;\">{WebUtility.HtmlEncode(subject)}</h2>");
        body.AppendLine("<table style=\"border-collapse:collapse;width:100%;max-width:640px;\">");

        foreach (var (label, value) in fields)
        {
            body.AppendLine("<tr>");
            body.AppendLine($"<td style=\"padding:8px 12px;border:1px solid #e2e8f0;background:#f8fafc;font-weight:600;width:35%;\">{WebUtility.HtmlEncode(label)}</td>");
            body.AppendLine($"<td style=\"padding:8px 12px;border:1px solid #e2e8f0;\">{WebUtility.HtmlEncode(value ?? "-")}</td>");
            body.AppendLine("</tr>");
        }

        body.AppendLine("</table>");
        body.AppendLine("<p style=\"margin-top:16px;font-size:12px;color:#666;\">Bu e-posta juniorleaguecup.com formu üzerinden gönderilmiştir.</p>");
        body.AppendLine("</body></html>");

        message.Body = new TextPart("html") { Text = body.ToString() };
        return message;
    }

    private static SmtpClient CreateClient()
    {
        var client = new SmtpClient { CheckCertificateRevocation = false };
        client.ServerCertificateValidationCallback = (_, _, _, _) => true;
        return client;
    }

    private static async Task ConnectAsync(SmtpClient client, EmailSettings settings)
    {
        var socketOptions = settings.SmtpPort switch
        {
            465 => SecureSocketOptions.SslOnConnect,
            587 => SecureSocketOptions.StartTls,
            _ => settings.UseSsl ? SecureSocketOptions.StartTlsWhenAvailable : SecureSocketOptions.Auto
        };

        await client.ConnectAsync(settings.SmtpHost, settings.SmtpPort, socketOptions);
    }
}
