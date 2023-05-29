using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace ergo_web2_2023.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly ILogger _logger;

        public EmailSender(IOptions<AuthMessageSenderOptions> optionsAccessor,
                       ILogger<EmailSender> logger)
        {
            Options = optionsAccessor.Value;
            _logger = logger;
        }

        public AuthMessageSenderOptions Options { get; }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            if (string.IsNullOrEmpty(Options.SendGridKey))
            {
                throw new Exception("Null SendGridKey");
            }
            await Execute(subject, htmlMessage,  email);
        }

        public async Task Execute(string subjectMessage,string htmlMessage, string toEmail)
        {
            var Sendgridkey = Environment.GetEnvironmentVariable("SENDGRID_KEY");
            var client = new SendGridClient(Sendgridkey);
            var from = new EmailAddress("fiel.elslander@student.vives.be", "Test verification");
            var subject = subjectMessage;
            var to = new EmailAddress(toEmail);
            var plainTextContent = "This is a test for verification";
            var htmlContent = htmlMessage;
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }
    }
}