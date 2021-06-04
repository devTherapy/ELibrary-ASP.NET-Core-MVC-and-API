using ELibrary.EmailServices.model.Mail;
using System;

namespace ELibrary.EmailServices
{
    public interface IEmailService
    {
        public bool SendEmail(Email email);
    }
}
