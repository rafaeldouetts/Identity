using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Domain.Domain
{
    public class ValidateTokenModel
    {
        public string Token { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; } // Caso queira validar pelo telefone
    }
}
