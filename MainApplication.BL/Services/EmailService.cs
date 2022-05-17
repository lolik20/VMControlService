using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace MainApplication.BL.Services
{
    public class EmailService
    {
        private readonly IConfiguration _configuration;
        private readonly SmtpClient _smtpClient;
        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
            _smtpClient = new SmtpClient("smtp.mail.ru", 587);
        }
        public void SendEmail(string message,string email)
        {
            _smtpClient.UseDefaultCredentials = false;
            _smtpClient.Credentials = new NetworkCredential("ffftttyyyy666@mail.ru", "111yyyytttfff");
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("ffftttyyyy666@mail.ru");
            mailMessage.To.Add(email);
            mailMessage.Body = message;
            mailMessage.Subject = "subject";
            _smtpClient.EnableSsl = true;
        }
    }
}
