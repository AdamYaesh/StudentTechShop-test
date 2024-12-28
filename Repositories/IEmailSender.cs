namespace StudentTechShop.API.Repositories
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string toEmail, string subject, string body);
    }
}
