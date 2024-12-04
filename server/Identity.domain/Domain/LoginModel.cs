using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Domain.Domain
{
    public class LoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class TokenModel
    {
        public TokenModel(string nome, string email, bool authenticated, bool twoFactorAuthenticated, string value)
        {
            Nome = nome;
            Email = email;
            Authenticated = authenticated;
            TwoFactorAuthenticated = twoFactorAuthenticated;
            Value = value;
        }

        public string Nome { get; set; }
        public string Email { get; set; }
        public bool Authenticated { get; set; }
        public bool TwoFactorAuthenticated { get; set; }
        public string Value { get; set; }
        public DateTime ValidTo { get; set; }
    }
}
