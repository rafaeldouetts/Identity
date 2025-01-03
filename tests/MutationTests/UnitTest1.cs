using Identity.Domain.Domain;
using System.ComponentModel.DataAnnotations;

namespace MutationTests
{
    public class UnitTest1
    {
        [Fact]
        public void TestEmailRequired()
        {
            // Arrange
            var model = new RegisterModel
            {
                Email = null
            };

            // Act
            var validationContext = new ValidationContext(model);
            var validationResults = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(model, validationContext, validationResults, true);

            // Assert
            Assert.False(isValid);
            Assert.Contains(validationResults, v => v.ErrorMessage == "O email é obrigatório.");
        }

        [Fact]
        public void TestEmailInvalidFormat()
        {
            // Arrange
            var model = new RegisterModel
            {
                Email = "invalid-email"
            };

            // Act
            var validationContext = new ValidationContext(model);
            var validationResults = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(model, validationContext, validationResults, true);

            // Assert
            Assert.False(isValid);
            Assert.Contains(validationResults, v => v.ErrorMessage == "Email inválido.");
        }

        [Fact]
        public void TestPasswordRequired()
        {
            // Arrange
            var model = new RegisterModel
            {
                Password = null
            };

            // Act
            var validationContext = new ValidationContext(model);
            var validationResults = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(model, validationContext, validationResults, true);

            // Assert
            Assert.False(isValid);
            Assert.Contains(validationResults, v => v.ErrorMessage == "A senha é obrigatória.");
        }

        [Fact]
        public void TestPasswordTooShort()
        {
            // Arrange
            var model = new RegisterModel
            {
                Password = "Short1!"
            };

            // Act
            var validationContext = new ValidationContext(model);
            var validationResults = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(model, validationContext, validationResults, true);

            // Assert
            Assert.False(isValid);
            Assert.Contains(validationResults, v => v.ErrorMessage == "A senha deve ter no mínimo 8 caracteres.");
        }

        [Fact]
        public void TestPasswordInvalidFormat()
        {
            // Arrange
            var model = new RegisterModel
            {
                Password = "password123"
            };

            // Act
            var validationContext = new ValidationContext(model);
            var validationResults = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(model, validationContext, validationResults, true);

            // Assert
            Assert.False(isValid);
            Assert.Contains(validationResults, v => v.ErrorMessage == "A senha deve conter pelo menos 8 caracteres, incluindo uma letra maiúscula, uma letra minúscula, um número e um símbolo.");
        }

        [Fact]
        public void TestPasswordConfirmationMismatch()
        {
            // Arrange
            var model = new RegisterModel
            {
                Password = "Password123!",
                ConfirmPassword = "Password124!"
            };

            // Act
            var validationContext = new ValidationContext(model);
            var validationResults = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(model, validationContext, validationResults, true);

            // Assert
            Assert.False(isValid);
            Assert.Contains(validationResults, v => v.ErrorMessage == "As senhas não coincidem.");
        }

        [Fact]
        public void TestPhoneInvalidFormat()
        {
            // Arrange
            var model = new RegisterModel
            {
                Telefone = "12345"  // Formato inválido
            };

            // Act
            var validationContext = new ValidationContext(model);
            var validationResults = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(model, validationContext, validationResults, true);

            // Assert
            Assert.False(isValid);
            Assert.Contains(validationResults, v => v.ErrorMessage == "Número de telefone inválido.");
        }

        [Fact]
        public void TestNameTooShort()
        {
            // Arrange
            var model = new RegisterModel
            {
                Nome = "John"  // Nome muito curto
            };

            // Act
            var validationContext = new ValidationContext(model);
            var validationResults = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(model, validationContext, validationResults, true);

            // Assert
            Assert.False(isValid);
            Assert.Contains(validationResults, v => v.ErrorMessage == "O nome deve ter no mínimo 8 caracteres.");
        }

    }
}