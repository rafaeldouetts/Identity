
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Controllers
{
    public class Require2FAAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // Verifica se o usuário está autenticado
            var user = context.HttpContext.User;

            if (user?.Identity == null || !user.Identity.IsAuthenticated)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            // Verifica se a claim "2fa_verified" está presente e válida
            var twoFactorClaim = user.Claims.FirstOrDefault(c => c.Type == "2fa_verified" && c.Value == "true");

            if (twoFactorClaim == null)
            {
                context.Result = new ForbidResult(); // Retorna 403 (Proibido) se a verificação de 2FA não foi feita
            }
        }
    }
}