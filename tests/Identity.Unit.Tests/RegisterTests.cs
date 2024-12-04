using FluentAssertions;
using Identity.Domain.Domain;
using System.ComponentModel.DataAnnotations;

namespace Identity.Unit.Tests
{
    public class RegisterTests
    {
        
        
        // Teste que valida se o campo Email é obrigatório
        [Fact]
        public void Email_ShouldBeRequired()
        {
            var model = new RegisterModel
            {
                Email = null
            };

            var validationResults = ValidateModel(model);

            validationResults.Should().ContainSingle(result => result.ErrorMessage == "O email é obrigatório.");
        }

        // Teste que valida se o email é válido
        [Fact]
        public void Email_ShouldBeValidEmail()
        {
            var model = new RegisterModel
            {
                Email = "invalid-email"
            };

            var validationResults = ValidateModel(model);

            validationResults.Should().ContainSingle(result => result.ErrorMessage == "Email inválido.");
        }

        // Teste que valida a senha com requisitos mínimos
        [Fact]
        public void Password_ShouldMeetRequirements()
        {
            var model = new RegisterModel
            {
                Password = "short"
            };

            var validationResults = ValidateModel(model);

            validationResults.Should().ContainSingle(result => result.ErrorMessage == "A senha deve ter no mínimo 8 caracteres.");

            model.Password = "Valid1@Password";
            validationResults = ValidateModel(model);

            validationResults.Should().BeEmpty(); // Se estiver tudo certo, não deve retornar erros.
        }

        // Teste que valida a confirmação de senha
        [Fact]
        public void ConfirmPassword_ShouldMatchPassword()
        {
            var model = new RegisterModel
            {
                Password = "Valid1@Password",
                ConfirmPassword = "Different1@Password"
            };

            var validationResults = ValidateModel(model);

            validationResults.Should().ContainSingle(result => result.ErrorMessage == "As senhas não coincidem.");
        }

        // Teste de validação de telefone
        [Fact]
        public void Phone_ShouldBeValid()
        {
            var model = new RegisterModel
            {
                Telefone = "123456789"
            };

            var validationResults = ValidateModel(model);

            validationResults.Should().ContainSingle(result => result.ErrorMessage == "Número de telefone inválido.");

            model.Telefone = "(11) 98765-4321"; // Número de telefone válido
            validationResults = ValidateModel(model);

            validationResults.Should().BeEmpty(); // Se o número de telefone for válido, não deve haver erro
        }

        // Teste que valida a data de nascimento
        [Fact]
        public void DataNascimento_ShouldBeRequiredAndValid()
        {
            var model = new RegisterModel
            {
                DataNascimento = null
            };

            var validationResults = ValidateModel(model);

            validationResults.Should().ContainSingle(result => result.ErrorMessage == "A data de nascimento é obrigatória.");

            model.DataNascimento = new DateTime(2000, 1, 1); // Data válida
            validationResults = ValidateModel(model);

            validationResults.Should().BeEmpty(); // Não deve haver erro para uma data válida
        }

        // Método para validar o modelo
        private static System.Collections.Generic.List<ValidationResult> ValidateModel(RegisterModel model)
        {
            var context = new ValidationContext(model);
            var validationResults = new System.Collections.Generic.List<ValidationResult>();
            Validator.TryValidateObject(model, context, validationResults, true);
            return validationResults;
        }
    }
}

