using System;
using System.Collections.Generic;
using System.Text;

namespace ELibrary.ViewModels
{
    public class ChangePasswordModel
    {
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }
    }
}
