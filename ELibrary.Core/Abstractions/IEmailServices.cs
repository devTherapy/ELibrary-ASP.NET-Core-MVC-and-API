using ELibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ELibrary.Core.Abstractions
{
    public interface IEmailServices
    {
        public bool SendEmail(Email email);
    }
}
