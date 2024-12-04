using Identity.Infra.Repositories.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Identity.Domain.Services
{
    public interface ITokenService
    {
        Task<string> GenerateEmailConfirmationTokenAsync(ApplicationUser user);
        Task<string> GeneratePhoneConfirmationTokenAsync(ApplicationUser user);
        Task<bool> ValidateTokenAsync(string token, string email = null, string phoneNumber = null);
    }

    public class TokenService : ITokenService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public TokenService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        // Gera o token de confirmação de e-mail
        public async Task<string> GenerateEmailConfirmationTokenAsync(ApplicationUser user)
        {
            return await _userManager.GenerateEmailConfirmationTokenAsync(user);
        }

        // Gera o token de confirmação de telefone
        public async Task<string> GeneratePhoneConfirmationTokenAsync(ApplicationUser user)
        {
            return await _userManager.GenerateChangePhoneNumberTokenAsync(user, user.PhoneNumber);
        }

        // Valida o token de confirmação
        public async Task<bool> ValidateTokenAsync(string token, string email = null, string phoneNumber = null)
        {
            if (email != null)
            {
                var user = await _userManager.FindByEmailAsync(email);
                return user != null && await _userManager.VerifyUserTokenAsync(user, "Default", "EmailConfirmation", token);
            }

            if (phoneNumber != null)
            {
                var user = await _userManager.Users
                             .FirstOrDefaultAsync(u => u.PhoneNumber == phoneNumber);

                return user != null && await _userManager.VerifyChangePhoneNumberTokenAsync(user, token, phoneNumber);
            }

            return false;
        }
    }
}
