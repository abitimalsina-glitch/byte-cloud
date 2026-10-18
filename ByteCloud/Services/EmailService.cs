using Resend;

namespace Email.Services;

public class EmailService
{
    private readonly IResend _resend;
    private readonly IConfiguration _configuration;

    public EmailService(
        IResend resend,
        IConfiguration configuration)
    {
        _resend = resend;
        _configuration = configuration;
    }

    public async Task SendEmailAsync(
        string to,
        string subject,
        string htmlBody)
    {
        var from = _configuration["Resend:FromEmail"]
            ?? throw new InvalidOperationException(
                "Resend sender email is not configured.");

        var message = new EmailMessage
        {
            From = from,
            Subject = subject,
            HtmlBody = htmlBody
        };

        message.To.Add(to);

        await _resend.EmailSendAsync(message);
    }
}
