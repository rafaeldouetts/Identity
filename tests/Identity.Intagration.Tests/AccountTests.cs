
using Identity.Intagration.Tests.Fixture;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Newtonsoft.Json;
using System.Text;

namespace Identity.Intagration.Tests
{
    public class AccountTests : IClassFixture<TestEnvironmentSetup>, IDisposable
    {
        private readonly HttpClient _client;

        public AccountTests(TestEnvironmentSetup setup)
        {
            _client = new HttpClient { BaseAddress = new System.Uri("http://localhost:5001/") };  // Endereço da API local
        }

        [Fact]
        public async Task Register_ShouldReturnOk_WhenValidDataIsProvided()
        {
            // Arrange
            var registerModel = new
            {
                Username = "testuser",
                Email = "testuser@example.com",
                Password = "Password123!"
            };

            var content = new StringContent(JsonConvert.SerializeObject(registerModel), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/account/register", content);

            // Assert
            //response.IsSuccessStatusCode.Should().BeTrue();
            //var responseContent = await response.Content.ReadAsStringAsync();
            //responseContent.Should().Contain("Registration successful");
        }

        //[Fact]
        //public async Task Login_ShouldReturnOk_WhenValidCredentialsAreProvided()
        //{
        //    // Arrange
        //    var loginModel = new
        //    {
        //        Email = "testuser@example.com",
        //        Password = "Password123!"
        //    };

        //    var content = new StringContent(JsonConvert.SerializeObject(loginModel), Encoding.UTF8, "application/json");

        //    // Act
        //    var response = await _client.PostAsync("/api/account/login", content);

        //    // Assert
        //    Assert.True(response.IsSuccessStatusCode);
        //    var responseContent = await response.Content.ReadAsStringAsync();
        //    //responseContent.Should().Contain("Login successful");
        //}

        [Fact]
        public async Task ChangePassword_ShouldReturnOk_WhenValidDataIsProvided()
        {
            // Arrange
            var changePasswordModel = new
            {
                OldPassword = "Password123!",
                NewPassword = "NewPassword123!"
            };

            var content = new StringContent(JsonConvert.SerializeObject(changePasswordModel), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/account/change-password", content);

            // Assert
            //response.IsSuccessStatusCode.Should().BeTrue();
            //var responseContent = await response.Content.ReadAsStringAsync();
            //responseContent.Should().Contain("Password changed successfully");
        }

        [Fact]
        public async Task ForgotPassword_ShouldReturnOk_WhenEmailIsValid()
        {
            // Arrange
            var forgotPasswordModel = new
            {
                Email = "testuser@example.com"
            };

            var content = new StringContent(JsonConvert.SerializeObject(forgotPasswordModel), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/account/forgot-password", content);

            // Assert
            //response.IsSuccessStatusCode.Should().BeTrue();
            //var responseContent = await response.Content.ReadAsStringAsync();
            //responseContent.Should().Contain("Password reset email sent");
        }

        [Fact]
        public async Task ValidateEmailToken_ShouldReturnOk_WhenValidTokenIsProvided()
        {
            // Arrange
            var validationModel = new
            {
                Token = "valid-token-here"
            };

            var content = new StringContent(JsonConvert.SerializeObject(validationModel), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/account/validate-email-token", content);

            // Assert
            //response.IsSuccessStatusCode.Should().BeTrue();
            //var responseContent = await response.Content.ReadAsStringAsync();
            //responseContent.Should().Contain("Email validated successfully");
        }

        public void Dispose()
        {
            
        }
    }
}