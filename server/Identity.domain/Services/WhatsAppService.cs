using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Domain.Services
{
    public interface IWhatsAppService
    {
        Task SendSmsAsync(string phoneNumber, string message);
    }

    public class WhatsAppService : IWhatsAppService
    {
        public async Task SendSmsAsync(string phoneNumber, string message)
        {
            // Lógica para enviar SMS (exemplo: usar Twilio, Nexmo, etc.)
            await Task.CompletedTask; // Simulando envio
        }
    }
}
