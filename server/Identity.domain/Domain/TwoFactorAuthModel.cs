using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Domain.Domain
{
    public class TwoFactorAuthModel
    {
        public string Email { get; set; }
        public string Method { get; set; } // "Email" ou "Phone"
    }
}
