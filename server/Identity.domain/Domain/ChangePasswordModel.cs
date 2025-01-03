using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Domain.Domain
{
    public class ChangePasswordModel
    {
        public ChangePasswordModel(string currentPassword, string newPassword)
        {
            CurrentPassword = currentPassword;
            NewPassword = newPassword;
        }
        public ChangePasswordModel()
        {
            
        }

        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
