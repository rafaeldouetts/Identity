using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Domain.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
    public class EmailService : IEmailService
    {
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            // Lógica para enviar e-mail (exemplo: usar o SMTP, SendGrid, etc.)
            await Task.CompletedTask; // Simulando envio
        }
    }
}
