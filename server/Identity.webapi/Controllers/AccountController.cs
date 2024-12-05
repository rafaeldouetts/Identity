using Identity.Domain.Domain;
using Identity.Infra.Repositories.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text;

using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Hosting;
using Identity.Blob;
using Identity.Domain.Services;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;

namespace Identity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly IBlobService _blobService;
        private readonly IRedisService _redisService;
        private readonly ITokenService _tokenService;
        private readonly IEmailService _emailService; 
        private readonly IWhatsAppService _whatsAppService;
        private readonly ITwoFactorAuthService _twoFactorAuthService;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration, IBlobService blobService, IRedisService redisService, ITokenService tokenService, IEmailService emailService, IWhatsAppService whatsAppService, ITwoFactorAuthService twoFactorAuthService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _blobService = blobService;
            _redisService = redisService;
            _tokenService = tokenService;
            _emailService = emailService;
            _whatsAppService = whatsAppService;
            _twoFactorAuthService = twoFactorAuthService;
        }


        // 1. Registro de usuário
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var user = new ApplicationUser { UserName = model.Nome, Email = model.Email, FullName = model.Nome, PhoneNumber = model.Telefone };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    return Ok(new { message = "Usuário registrado com sucesso!" });
                }

                return BadRequest(result.Errors);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // 2. Login de usuário
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
                return Unauthorized("Usuário ou senha inválidos");

            var block = await _redisService.IsLoginBlockedAsync(model.Email);

            if (block)
                return Unauthorized("Usuário ou senha inválidos");

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);

            if (result.Succeeded)
            {
                var authClaims = new List<Claim>
            {
                new (ClaimTypes.Name, user.UserName),
                new (JwtRegisteredClaimNames.Jti, user.Id)
            };
                var token = GetToken(authClaims, user);

                await _redisService.ResetLoginAttemptsAsync(model.Email);

                Console.WriteLine($"Token gerado: {token.Value}");

                return Ok(token);
            }

            await _redisService.IncrementLoginAttemptAsync(model.Email);

            return Unauthorized("Usuário ou senha inválidos");
        }

        // 3. Logout
        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok(new { message = "Logout realizado com sucesso!" });
        }

        // 4. Trocar Senha
        [HttpPost("change-password")]
        [Authorize]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordModel model)
        {
            var user = await GetUser();

            if (user == null)
                return Unauthorized("Usuário não encontrado");

            var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);

            if (result.Succeeded)
                return Ok(new { message = "Senha alterada com sucesso!" });

            return BadRequest(result.Errors);
        }

        // 5. Recuperar Senha
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
                return BadRequest("Não encontramos um usuário com este e-mail.");

            // Gerar o token de redefinição de senha
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            // Gerar um identificador único curto para esse token
            var resetId = Guid.NewGuid().ToString("N"); // "N" remove os hífens para deixar o GUID mais curto

            // Armazenar o token no Redis com esse identificador curto
            await _redisService.SetValueAsync($"reset_token_{resetId}", token, TimeSpan.FromMinutes(10));

            // Gerar o link de redefinição com o identificador curto
            var resetLink = $"http://localhost:4200/account/change-password/{resetId}";

            // Enviar o link por e-mail
            await _emailService.SendEmailAsync(model.Email, "Recuperação de Senha", resetLink);

            return Ok("Instruções de recuperação de senha enviadas.");
        }

        // 6. Redefinir Senha
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordModel model)
        {
            // Valida o modelo
            if (!ModelState.IsValid)
                return BadRequest("Dados inválidos.");

            // Verifica se o token gerado para o e-mail existe no Redis
            var tokenFromRedis = await _redisService.GetValueAsync($"reset_token_{model.Token}");


            if (string.IsNullOrEmpty(tokenFromRedis))
                return BadRequest("Token de recuperação expirado ou inválido.");

            // Encontra o usuário com o e-mail fornecido
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
                return BadRequest("Não encontramos um usuário com este e-mail");

            // Verifica se o token fornecido é válido
            var resetResult = await _userManager.ResetPasswordAsync(user, tokenFromRedis, model.NewPassword);

            if (!resetResult.Succeeded)
            {
                return BadRequest("Falha ao redefinir a senha. Verifique o token ou a nova senha.");
            }

            // Se a redefinição de senha foi bem-sucedida, retorna um status positivo
            return Ok(new { message = "Senha redefinida com sucesso." });
        }

        // 7. Atualizar Perfil
        [HttpPut("update-profile")]
        [Authorize]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileModel model)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
                return Unauthorized("Usuário não encontrado");

            user.UserName = model.UserName ?? user.UserName;
            user.Email = model.Email ?? user.Email;

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
                return Ok(new { message = "Perfil atualizado com sucesso!" });

            return BadRequest(result.Errors);
        }

        // 8. Atualizar Foto Perfil
        [HttpPost("upload-profile-picture")]
        [Authorize]
        public async Task<IActionResult> UploadProfilePicture(IFormFile formFile)
        {
            var user = await _userManager.GetUserAsync(User);

            user.ProfilePictureUrl = await _blobService.Upload(formFile);

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
                return Ok(new { message = "Perfil atualizado com sucesso!" });

            return BadRequest(result.Errors);
        }

        // Endpoint para disparar o token de confirmação de e-mail
        [HttpPost("send-email-confirmation")]
        [Authorize]
        public async Task<IActionResult> SendEmailConfirmation([FromBody] SendConfirmationTokenModel model)
        {
            var user = await GetUser();

            if (user == null)
                return BadRequest("Usuário não encontrado");

            var token = await _tokenService.GenerateEmailConfirmationTokenAsync(user);

            var guid = Guid.NewGuid();

            // Enviar o token para o e-mail do usuário
            await _emailService.SendEmailAsync(user.Email, "Confirmação de E-mail", $"Seu token de confirmação é: {guid}");

            await _redisService.SetValueAsync($"token_email_confirmation_{guid}", token, TimeSpan.FromMinutes(10));

            return Ok(new { message = "Token de confirmação de e-mail enviado." });
        }

        // Endpoint para disparar o token de confirmação de telefone
        [HttpPost("send-phone-confirmation")]
        [Authorize]
        public async Task<IActionResult> SendPhoneConfirmation([FromBody] SendConfirmationTokenModel model)
        {
            var user = await GetUser();

            if (model.PhoneNumber != user.PhoneNumber)
                return BadRequest("Usuário não encontrado");

            if (user == null)
                return BadRequest("Usuário não encontrado");

            var token = await _tokenService.GeneratePhoneConfirmationTokenAsync(user);

            // Enviar o token por SMS para o número de telefone
            await _whatsAppService.SendSmsAsync(user.PhoneNumber, $"Seu token de confirmação é: {token}");

            await _redisService.SetValueAsync($"token_PhoneNumber_confirmation_{model.PhoneNumber}", token, TimeSpan.FromMinutes(10));

            return Ok(new { message = "Token de confirmação de telefone enviado." });
        }

        // Endpoint para validar o token de confirmação de e-mail
        [HttpPost("validate-email-token")]
        [Authorize]
        public async Task<IActionResult> ValidateEmailToken([FromBody] ValidateTokenModel model)
        {
            var emailTokenFromRedis = await _redisService.GetValueAsync($"token_email_confirmation_{model.Token}");

            if (string.IsNullOrEmpty(emailTokenFromRedis))
                return BadRequest("Token de validação expirado ou inválido.");

            var isValid = await _tokenService.ValidateTokenAsync(emailTokenFromRedis, email: model.Email);

            if (!isValid)
                return BadRequest("Token de confirmação de e-mail inválido");

            var user = await GetUser();

            await _userManager.ConfirmEmailAsync(user, model.Token);

            return Ok(new { message = "E-mail confirmado com sucesso." });
        }

        // Endpoint para validar o token de confirmação de telefone
        [HttpPost("validate-phone-token")]
        [Authorize]
        public async Task<IActionResult> ValidatePhoneToken([FromBody] ValidateTokenModel model)
        {
            var phoneTokenFromRedis = await _redisService.GetValueAsync($"token_PhoneNumber_confirmation_{model.PhoneNumber}");

            if (string.IsNullOrEmpty(phoneTokenFromRedis))
                return BadRequest("Token de validação expirado ou inválido.");


            var isValid = await _tokenService.ValidateTokenAsync(model.Token, phoneNumber: model.PhoneNumber);

            if (!isValid)
                return BadRequest("Token de confirmação de telefone inválido");

            var user = await GetUser();

            await _userManager.ChangePhoneNumberAsync(user, model.PhoneNumber, model.Token);

            return Ok(new { message = "Telefone confirmado com sucesso." });
        }

        // Endpoint para iniciar o processo de 2FA
        [HttpPost("send-2fa-code")]
        public async Task<IActionResult> SendTwoFactorCode([FromBody] TwoFactorAuthModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            user.TwoFactorEnabled = true;

            if (user == null)
                return BadRequest("Usuário não encontrado.");

            // Verifica se o 2FA está habilitado
            if (!await _userManager.GetTwoFactorEnabledAsync(user))
                return BadRequest("2FA não está habilitado para este usuário.");

            // Escolhe o método de envio: email ou SMS
            string method = model.Method; // "Email" ou "Phone"
            if (method == "Email" && string.IsNullOrEmpty(user.Email))
                return BadRequest("E-mail não está configurado.");

            if (method == "Phone" && string.IsNullOrEmpty(user.PhoneNumber))
                return BadRequest("Número de telefone não está configurado.");

            // Gera e envia o código de 2FA
            var code = await _twoFactorAuthService.GenerateAndSendTwoFactorCodeAsync(user, method);

            return Ok(new { message = "Código de 2FA enviado." });
        }

        // Endpoint para validar o código de 2FA
        [HttpPost("validate-fa-code")]
        [Authorize]
        public async Task<IActionResult> ValidateTwoFactorCode([FromBody] ValidateTwoFactorAuthRequest model)
        {
            var user = await GetUser();

            if (user == null)
                return BadRequest("Usuário não encontrado.");

            // Valida o código de 2FA
            var isValid = await _twoFactorAuthService.ValidateTwoFactorCodeAsync(user, model.Code, model.Method);

            if (!isValid)
                return BadRequest("Código de 2FA inválido.");

            // Gera um novo token JWT indicando que o 2FA foi validado
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("2fa_verified", "true") // Adiciona uma claim indicando 2FA validado
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1), // Define a validade do token
                signingCredentials: creds
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(new { message = "Autenticação de 2FA bem-sucedida.", token = tokenString });
        }

        private async Task<ApplicationUser> GetUser()
        {
            var jtiClaim = User.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti);

            if (jtiClaim == null) return null;

            return await _userManager.FindByIdAsync(jtiClaim.Value);
        }

        [HttpDelete("delete-account")]
        [Authorize]
        public async Task<IActionResult> DeleteAccount()
        {
            // Recupera o usuário autenticado
            var user = await GetUser();

            // Verifica se o usuário está autenticado
            if (user == null)
            {
                return Unauthorized("Usuário não autenticado.");
            }

            // Atualiza o e-mail para um padrão
            user.Email = "defaultemail@example.com"; // E-mail padrão
            user.UserName = "Usuário Anônimo"; // Nome padrão

            // Atualiza o usuário no banco de dados
            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                return BadRequest("Falha ao excluir os dados do usuário.");
            }

            // Caso a atualização seja bem-sucedida
            return Ok(new { message = "Sua conta foi excluída." });
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAccount()
        {
            // Recupera o usuário autenticado
            var user = await _userManager.GetUserAsync(User);

            // Verifica se o usuário está autenticado
            if (user == null)
            {
                return Unauthorized("Usuário não autenticado.");
            }

            var result = new UserModel(user.FullName, user.Email);

            return Ok(result);
        }

        [HttpPost("validate-token")]
        [Authorize]
        public IActionResult ValidateToken([FromBody] ValidateTokenJWTModel model)
        {
            if (string.IsNullOrEmpty(model.Token))
            {
                return BadRequest("O token não pode ser vazio.");
            }

            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();

                // Verifica se é um JWT válido
                if (!tokenHandler.CanReadToken(model.Token))
                {
                    return BadRequest("Token inválido.");
                }

                // Configura os parâmetros de validação
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = _configuration["Jwt:Issuer"],

                    ValidateAudience = true,
                    ValidAudience = _configuration["Jwt:Audience"],

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"])),

                    ValidateLifetime = true, // Valida a expiração do token
                    ClockSkew = TimeSpan.Zero // Define tolerância de tempo como 0
                };

                // Valida o token
                tokenHandler.ValidateToken(model.Token, validationParameters, out var validatedToken);

                return Ok(new { isValid = true, message = "Token válido." });
            }
            catch (SecurityTokenExpiredException)
            {
                return Unauthorized(new { isValid = false, message = "Token expirado." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { isValid = false, message = $"Token inválido: {ex.Message}" });
            }
        }


        // Método auxiliar para gerar o token JWT
        private TokenModel GenerateJwtToken(ApplicationUser user)
        {
            user.TwoFactorEnabled = true;

            // Adicionando as claims padrão
            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, user.Id),
        new Claim(ClaimTypes.Name, user.UserName),
        new Claim(ClaimTypes.Email, user.Email),
    };

            // Verificando se o 2FA foi validado
            if (user.TwoFactorEnabled)
            {
                // Adiciona uma claim para indicar que o 2FA foi validado
                claims.Add(new Claim("TwoFactorValidated", "true"));
            }
            else
            {
                claims.Add(new Claim("TwoFactorValidated", "false"));
            }

            // Chave secreta para assinatura do token
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Gerando o token JWT
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(1),  // Expira após 1 dia
                signingCredentials: creds
            );

            // Convertendo o token para string
            var value = new JwtSecurityTokenHandler().WriteToken(token);

            // Retornando o token junto com o nome de usuário, e-mail e status do 2FA
            return new TokenModel(user.UserName, user.Email, true, user.TwoFactorEnabled, value);
        }

        private TokenModel GetToken(List<Claim> authClaims, IdentityUser user)
        {
            try
            {
                //obtém a chave de assinatura do JWT
                var authSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]));

                //Monta o TOKEN
                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidIssuer"],
                    expires: DateTime.Now.AddHours(1),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

                //Retorna o token + validade
                return new TokenModel(user.UserName, user.Email, true, false, new JwtSecurityTokenHandler().WriteToken(token));
            }
            catch (Exception ex)
            {

                throw;
            }
        }

    }
}
