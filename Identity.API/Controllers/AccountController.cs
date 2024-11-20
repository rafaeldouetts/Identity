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

namespace Identity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;

        // 1. Registro de usuário
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                return Ok(new { message = "Usuário registrado com sucesso!" });
            }

            return BadRequest(result.Errors);
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

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);

            if (result.Succeeded)
            {
                var token = GenerateJwtToken(user);
                return Ok(new { token });
            }

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
            var user = await _userManager.GetUserAsync(User);

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
                return BadRequest("Não encontramos um usuário com este e-mail");

            // Lógica para envio do e-mail de recuperação de senha (com link)
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            // Aqui você enviaria o token por e-mail para o usuário

            return Ok(new { message = "Link de recuperação de senha enviado." });
        }

        // 6. Atualizar Perfil
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

        // Endpoint para atualizar a foto de perfil do usuário
        [HttpPost("upload-profile-picture")]
        public async Task<IActionResult> UploadProfilePicture(IFormFile formFile)
        {
            if (formFile == null )
            {
                return BadRequest("Nenhuma foto foi fornecida.");
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized("Usuário não encontrado.");
            }

            // Gerar o caminho do arquivo no servidor
            var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", "profile_pictures");

            // Criar o diretório caso não exista
            Directory.CreateDirectory(filePath);

            // Criar um nome único para o arquivo
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.ProfilePicture.FileName);

            // Caminho completo do arquivo
            var fileSavePath = Path.Combine(filePath, fileName);

            // Salvar o arquivo no diretório
            using (var stream = new FileStream(fileSavePath, FileMode.Create))
            {
                await model.ProfilePicture.CopyToAsync(stream);
            }

            // Atualizar o caminho da foto de perfil do usuário
            user.ProfilePictureUrl = $"/uploads/profile_pictures/{fileName}";
            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                return BadRequest("Erro ao atualizar a foto de perfil.");
            }

            return Ok(new { ProfilePictureUrl = user.ProfilePictureUrl });
        }

        // Método auxiliar para gerar o token JWT
        private string GenerateJwtToken(ApplicationUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }    
}
