using System;
using System.Collections.Generic;
using System.Text;

namespace ELibrary.Dtos
{
    public class ConfirmationEmailDto
    {
        public string userid { get; set; }
        public string token { get; set; }
    }
}
