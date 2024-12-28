using System.Net.Mail;
using System.Net;

namespace StudentTechShop.API.Repositories
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration configuration;

        public EmailSender(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            var smtpClient = new SmtpClient(configuration["Smtp:Host"])
            {
                Port = int.Parse(configuration["Smtp:Port"]),
                Credentials = new NetworkCredential(configuration["Smtp:Username"], configuration["Smtp:Password"]),
                EnableSsl = true,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(configuration["Smtp:FromEmail"]),
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
            };

            mailMessage.To.Add(toEmail);

            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}

