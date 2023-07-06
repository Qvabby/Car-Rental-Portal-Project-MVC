using Google.Apis.Gmail.v1.Data;
using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net;
using System.Net.Mail;

namespace Car_Rental_Portal_Project_MVC.Services.Implementations
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            var Email = "itsteptest@gmail.com";
            var password = "Tbilisi123!";
            var client = new SmtpClient("smtp.ofice365.com", 587)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(Email,password)            
            };
            var mailMessage = new MailMessage(from: Email,to:email,subject,message);

            return client.SendMailAsync(mailMessage);
        }
    }
}
