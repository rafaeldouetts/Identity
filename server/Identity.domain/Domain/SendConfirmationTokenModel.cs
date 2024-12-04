using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Domain.Domain
{
    public class SendConfirmationTokenModel
    {
        public string Email { get; set; }
        public string PhoneNumber { get; set; } // Caso queira enviar para o telefone também
    }
}
