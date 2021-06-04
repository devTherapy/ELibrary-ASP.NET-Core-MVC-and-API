using System;
using System.Collections.Generic;
using System.Text;

namespace ELibrary.Models.AppsettingModels
{
    public class EmailSettings
    {
        public string From { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
    }
}
