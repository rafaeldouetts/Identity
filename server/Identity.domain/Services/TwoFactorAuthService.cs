using Identity.Infra.Repositories.Context;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Domain.Services
{
    public interface ITwoFactorAuthService 
    {
        Task<string> GenerateAndSendTwoFactorCodeAsync(ApplicationUser user, string method);
        Task<bool> ValidateTwoFactorCodeAsync(ApplicationUser user, string code, string method);
    }


    public class TwoFactorAuthService : ITwoFactorAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailService _emailService;
        //private readonly IWhatsAppService _whatsAppService;

        public TwoFactorAuthService(UserManager<ApplicationUser> userManager, IEmailService emailService/*, IWhatsAppService whatsAppService*/)
        {
            _userManager = userManager;
            //_whatsAppService = whatsAppService;
            _emailService = emailService;
        }

        // Gera e envia o código de 2FA (por email ou SMS)
        public async Task<string> GenerateAndSendTwoFactorCodeAsync(ApplicationUser user, string method)
        {
            var code = await _userManager.GenerateTwoFactorTokenAsync(user, method);

            // Enviar o código por e-mail ou SMS (dependendo do método de 2FA configurado)
            if (method == "Email")
            {
                // Enviar por e-mail
                // Aqui você pode usar seu serviço de e-mail
                await _emailService.SendEmailAsync(user.Email, "Código de Autenticação", $"Seu código de 2FA é: {code}");
            }
            else if (method == "Phone")
            {
                // Enviar por SMS
                //await _whatsAppService.SendSmsAsync(user.PhoneNumber, $"Seu código de 2FA é: {code}");
            }

            return code;
        }

        // Valida o código de 2FA fornecido pelo usuário
        public async Task<bool> ValidateTwoFactorCodeAsync(ApplicationUser user, string code, string method)
        {
            var isValid = await _userManager.VerifyTwoFactorTokenAsync(user, method, code);
            return isValid;
        }
    }
}
