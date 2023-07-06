using Car_Rental_Portal_Project_MVC.Services.Interfaces;
using Google.Apis.Gmail.v1.Data;
using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net;
using System.Net.Mail;

namespace Car_Rental_Portal_Project_MVC.Services.Implementations
{
    public class EmailSender : IEmailService
    {
		public readonly IConfiguration configuration;
		public EmailSender(IConfiguration con)
		{
			configuration = con;
		}
		public Task SendEmailAsync(string email, string subject, string message)
        {
            //Getting Options From EmailSenderOptions.
			var options = configuration.GetSection("Credentials").Get<EmailSenderOptions>();
            //Creating Client
			var client = new SmtpClient("smtp.office365.com", 587)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(options.Email, options.Password)
			};
            var mailMessage = new MailMessage(from: options.Email,to:email,subject,message);
            mailMessage.IsBodyHtml = true;

            return client.SendMailAsync(mailMessage);
        }
    }
}
