using Identity.Domain.Domain;

namespace Identity.Unit.Tests
{
    public class ChangePasswordModelTests
    {
        [Fact]
        public void ChangePasswordModel_SetProperties_PropertiesShouldBeSet()
        {
            // Arrange
            var currentPassword = "currentPass123";
            var newPassword = "newPass123";

            // Act
            var model = new ChangePasswordModel(currentPassword, newPassword);

            // Assert
            Assert.Equal(currentPassword, model.CurrentPassword);
            Assert.Equal(newPassword, model.NewPassword);
        }

        [Fact]
        public void ChangePasswordModel_DefaultValues_ShouldBeNull()
        {
            // Arrange
            var model = new ChangePasswordModel();

            // Assert
            Assert.Null(model.CurrentPassword);
            Assert.Null(model.NewPassword);
        }
    }
}