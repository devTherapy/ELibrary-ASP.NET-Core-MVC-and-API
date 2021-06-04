using ELibrary.Core.Abstractions;
using ELibrary.Models;
using ELibrary.Models.AppsettingModels;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace ELibrary.Core.Implementations
{

    public class EmailServices : IEmailServices
    {
        private readonly EmailSettings _mailSetting;

        public EmailServices(IOptions<EmailSettings> mailSettings)
        {
            _mailSetting = mailSettings.Value;
        }
        public bool SendEmail(Email email)
        {
            MailMessage mailMessage = new MailMessage { From = new MailAddress(_mailSetting.From) };
            mailMessage.To.Add(new MailAddress(email.To));


            //sets email content
            mailMessage.Subject = "Confirm your email";
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = email.Body;

            //sets up smtp client
            SmtpClient client = new SmtpClient
            {
                UseDefaultCredentials = false,
                EnableSsl = true,
                Host = _mailSetting.Host,
                Port = _mailSetting.Port,
                Credentials = new System.Net.NetworkCredential(_mailSetting.From, _mailSetting.Password),
                DeliveryMethod = SmtpDeliveryMethod.Network
            };

            try
            {
                //send mail
                client.Send(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                // log exception
                Console.WriteLine(ex.Message);
            }

            return false;
        }

       
    }
}
