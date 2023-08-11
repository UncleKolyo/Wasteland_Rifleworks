using NUnit.Framework;
using System.ComponentModel.DataAnnotations;
using WastelandRifleworks.Web.ViewModels.Engineer;
using WastelandRifleworks.Web.ViewModels.Tag;
using WastelandRifleworks.Web.ViewModels.Type;
using WastelandRifleworks.Web.ViewModels.User;
using WastelandRifleworks.Web.ViewModels.Weapon;
using WastelandRilfeworks.Data.Models;
using static WastelandGeneralConstants.WastelandGeneralConstants;

namespace WastelandRifleworks.Tests.ViewModels
{
    [TestFixture]
    public class BecomeEngineerFormModelTests
    {
        [Test]
        public void Username_ShouldSetAndGetCorrectly()
        {
            // Arrange
            var model = new BecomeEngineerFormModel();
            var expectedUsername = "testUsername";

            // Act
            model.Username = expectedUsername;

            // Assert
            Assert.AreEqual(expectedUsername, model.Username);
        }

        [Test]
        public void Aprovement_ShouldSetAndGetCorrectly()
        {
            // Arrange
            var model = new BecomeEngineerFormModel();
            var expectedAprovement = 42;

            // Act
            model.Aprovement = expectedAprovement;

            // Assert
            Assert.AreEqual(expectedAprovement, model.Aprovement);
        }

        [TestFixture]
        public class WeaponTagFormModelTests
        {
            [Test]
            public void Id_SetGet_ShouldWork()
            {
                // Arrange
                var model = new WeaponTagFormModel();
                int expectedId = 42;

                // Act
                model.Id = expectedId;
                int actualId = model.Id;

                // Assert
                Assert.AreEqual(expectedId, actualId);
            }

            [Test]
            public void Name_SetGet_ShouldWork()
            {
                // Arrange
                var model = new WeaponTagFormModel();
                string expectedName = "TestTag";

                // Act
                model.Name = expectedName;
                string actualName = model.Name;

                // Assert
                Assert.AreEqual(expectedName, actualName);
            }
        }
        [TestFixture]
        public class WeaponTypeFormModelTests
        {
            [Test]
            public void Id_SetGet_ShouldWork()
            {
                // Arrange
                var model = new WeaponTypeFormModel();
                int expectedId = 42;

                // Act
                model.Id = expectedId;
                int actualId = model.Id;

                // Assert
                Assert.AreEqual(expectedId, actualId);
            }

            [Test]
            public void Name_SetGet_ShouldWork()
            {
                // Arrange
                var model = new WeaponTypeFormModel();
                string expectedName = "TestType";

                // Act
                model.Name = expectedName;
                string actualName = model.Name;

                // Assert
                Assert.AreEqual(expectedName, actualName);
            }
        }
        [TestFixture]
        public class LoginFormModelTests
        {
            [Test]
            public void Email_SetGet_ShouldWork()
            {
                // Arrange
                var model = new LoginFormModel();
                string expectedEmail = "test@example.com";

                // Act
                model.Email = expectedEmail;
                string actualEmail = model.Email;

                // Assert
                Assert.AreEqual(expectedEmail, actualEmail);
            }

            [Test]
            public void Password_SetGet_ShouldWork()
            {
                // Arrange
                var model = new LoginFormModel();
                string expectedPassword = "testPassword";

                // Act
                model.Password = expectedPassword;
                string actualPassword = model.Password;

                // Assert
                Assert.AreEqual(expectedPassword, actualPassword);
            }

            [Test]
            public void RememberMe_SetGet_ShouldWork()
            {
                // Arrange
                var model = new LoginFormModel();
                bool expectedRememberMe = true;

                // Act
                model.RememberMe = expectedRememberMe;
                bool actualRememberMe = model.RememberMe;

                // Assert
                Assert.AreEqual(expectedRememberMe, actualRememberMe);
            }

            [Test]
            public void ReturnUrl_SetGet_ShouldWork()
            {
                // Arrange
                var model = new LoginFormModel();
                string expectedReturnUrl = "/dashboard";

                // Act
                model.ReturnUrl = expectedReturnUrl;
                string actualReturnUrl = model.ReturnUrl;

                // Assert
                Assert.AreEqual(expectedReturnUrl, actualReturnUrl);
            }

            [Test]
            public void Email_RequiredAttribute_ShouldHaveRequiredValidation()
            {
                // Arrange
                var propertyInfo = typeof(LoginFormModel).GetProperty(nameof(LoginFormModel.Email));

                // Act
                var requiredAttribute = propertyInfo.GetCustomAttributes(typeof(RequiredAttribute), false);

                // Assert
                Assert.AreEqual(1, requiredAttribute.Length);
            }

            [Test]
            public void Email_EmailAddressAttribute_ShouldHaveEmailAddressValidation()
            {
                // Arrange
                var propertyInfo = typeof(LoginFormModel).GetProperty(nameof(LoginFormModel.Email));

                // Act
                var emailAddressAttribute = propertyInfo.GetCustomAttributes(typeof(EmailAddressAttribute), false);

                // Assert
                Assert.AreEqual(1, emailAddressAttribute.Length);
            }

            [Test]
            public void Password_RequiredAttribute_ShouldHaveRequiredValidation()
            {
                // Arrange
                var propertyInfo = typeof(LoginFormModel).GetProperty(nameof(LoginFormModel.Password));

                // Act
                var requiredAttribute = propertyInfo.GetCustomAttributes(typeof(RequiredAttribute), false);

                // Assert
                Assert.AreEqual(1, requiredAttribute.Length);
            }

            [Test]
            public void Password_DataTypeAttribute_ShouldHavePasswordValidation()
            {
                // Arrange
                var propertyInfo = typeof(LoginFormModel).GetProperty(nameof(LoginFormModel.Password));

                // Act
                var dataTypeAttribute = propertyInfo.GetCustomAttributes(typeof(DataTypeAttribute), false);

                // Assert
                Assert.AreEqual(1, dataTypeAttribute.Length);
                Assert.AreEqual(DataType.Password, ((DataTypeAttribute)dataTypeAttribute[0]).DataType);
            }

            [Test]
            public void RememberMe_DisplayAttribute_ShouldHaveCorrectDisplayName()
            {
                // Arrange
                var propertyInfo = typeof(LoginFormModel).GetProperty(nameof(LoginFormModel.RememberMe));

                // Act
                var displayAttribute = propertyInfo.GetCustomAttributes(typeof(DisplayAttribute), false);

                // Assert
                Assert.AreEqual(1, displayAttribute.Length);
                Assert.AreEqual("Remember me?", ((DisplayAttribute)displayAttribute[0]).Name);
            }
        }

        [TestFixture]
        public class RegisterFormModelTests
        {
            [Test]
            public void Email_SetGet_ShouldWork()
            {
                // Arrange
                var model = new RegisterFormModel();
                string expectedEmail = "test@example.com";

                // Act
                model.Email = expectedEmail;
                string actualEmail = model.Email;

                // Assert
                Assert.AreEqual(expectedEmail, actualEmail);
            }

            [Test]
            public void Password_SetGet_ShouldWork()
            {
                // Arrange
                var model = new RegisterFormModel();
                string expectedPassword = "testPassword";

                // Act
                model.Password = expectedPassword;
                string actualPassword = model.Password;

                // Assert
                Assert.AreEqual(expectedPassword, actualPassword);
            }

            [Test]
            public void ConfirmPassword_SetGet_ShouldWork()
            {
                // Arrange
                var model = new RegisterFormModel();
                string expectedConfirmPassword = "testPassword";

                // Act
                model.ConfirmPassword = expectedConfirmPassword;
                string actualConfirmPassword = model.ConfirmPassword;

                // Assert
                Assert.AreEqual(expectedConfirmPassword, actualConfirmPassword);
            }

            [Test]
            public void Email_RequiredAttribute_ShouldHaveRequiredValidation()
            {
                // Arrange
                var propertyInfo = typeof(RegisterFormModel).GetProperty(nameof(RegisterFormModel.Email));

                // Act
                var requiredAttribute = propertyInfo.GetCustomAttributes(typeof(RequiredAttribute), false);

                // Assert
                Assert.AreEqual(1, requiredAttribute.Length);
            }

            [Test]
            public void Email_EmailAddressAttribute_ShouldHaveEmailAddressValidation()
            {
                // Arrange
                var propertyInfo = typeof(RegisterFormModel).GetProperty(nameof(RegisterFormModel.Email));

                // Act
                var emailAddressAttribute = propertyInfo.GetCustomAttributes(typeof(EmailAddressAttribute), false);

                // Assert
                Assert.AreEqual(1, emailAddressAttribute.Length);
            }

            [Test]
            public void Password_RequiredAttribute_ShouldHaveRequiredValidation()
            {
                // Arrange
                var propertyInfo = typeof(RegisterFormModel).GetProperty(nameof(RegisterFormModel.Password));

                // Act
                var requiredAttribute = propertyInfo.GetCustomAttributes(typeof(RequiredAttribute), false);

                // Assert
                Assert.AreEqual(1, requiredAttribute.Length);
            }

            [Test]
            public void Password_DataTypeAttribute_ShouldHavePasswordValidation()
            {
                // Arrange
                var propertyInfo = typeof(RegisterFormModel).GetProperty(nameof(RegisterFormModel.Password));

                // Act
                var dataTypeAttribute = propertyInfo.GetCustomAttributes(typeof(DataTypeAttribute), false);

                // Assert
                Assert.AreEqual(1, dataTypeAttribute.Length);
                Assert.AreEqual(DataType.Password, ((DataTypeAttribute)dataTypeAttribute[0]).DataType);
            }

            [Test]
            public void ConfirmPassword_DataTypeAttribute_ShouldHavePasswordValidation()
            {
                // Arrange
                var propertyInfo = typeof(RegisterFormModel).GetProperty(nameof(RegisterFormModel.ConfirmPassword));

                // Act
                var dataTypeAttribute = propertyInfo.GetCustomAttributes(typeof(DataTypeAttribute), false);

                // Assert
                Assert.AreEqual(1, dataTypeAttribute.Length);
                Assert.AreEqual(DataType.Password, ((DataTypeAttribute)dataTypeAttribute[0]).DataType);
            }

            [Test]
            public void ConfirmPassword_CompareAttribute_ShouldHaveCompareValidation()
            {
                // Arrange
                var propertyInfo = typeof(RegisterFormModel).GetProperty(nameof(RegisterFormModel.ConfirmPassword));

                // Act
                var compareAttribute = propertyInfo.GetCustomAttributes(typeof(CompareAttribute), false);

                // Assert
                Assert.AreEqual(1, compareAttribute.Length);
                var attribute = (CompareAttribute)compareAttribute[0];
                Assert.AreEqual("Password", attribute.OtherProperty);
                Assert.AreEqual("The password and confirmation password do not match.", attribute.ErrorMessage);
            }

            [Test]
            public void ConfirmPassword_DisplayAttribute_ShouldHaveCorrectDisplayName()
            {
                // Arrange
                var propertyInfo = typeof(RegisterFormModel).GetProperty(nameof(RegisterFormModel.ConfirmPassword));

                // Act
                var displayAttribute = propertyInfo.GetCustomAttributes(typeof(DisplayAttribute), false);

                // Assert
                Assert.AreEqual(1, displayAttribute.Length);
                Assert.AreEqual("Confirm password", ((DisplayAttribute)displayAttribute[0]).Name);
            }

            [TestFixture]
            public class WeaponFormModelTests
            {
                [Test]
                public void Name_SetGet_ShouldWork()
                {
                    // Arrange
                    var model = new WeaponFormModel();
                    string expectedName = "Sample Weapon";

                    // Act
                    model.Name = expectedName;
                    string actualName = model.Name;

                    // Assert
                    Assert.AreEqual(expectedName, actualName);
                }

                [Test]
                public void ShortDescription_SetGet_ShouldWork()
                {
                    // Arrange
                    var model = new WeaponFormModel();
                    string expectedDescription = "Short description";

                    // Act
                    model.ShortDescription = expectedDescription;
                    string actualDescription = model.ShortDescription;

                    // Assert
                    Assert.AreEqual(expectedDescription, actualDescription);
                }

                // Similar tests for other properties...

                [Test]
                public void Types_SetGet_ShouldWork()
                {
                    // Arrange
                    var model = new WeaponFormModel();
                    var expectedTypes = new List<WeaponTypeFormModel>
            {
                new WeaponTypeFormModel { Id = 1, Name = "Assault Rifle" },
                new WeaponTypeFormModel { Id = 2, Name = "Sniper Rifle" }
            };

                    // Act
                    model.Types = expectedTypes.ToList();
                    var actualTypes = model.Types.ToList();

                    // Assert
                    Assert.AreEqual(expectedTypes.Count, actualTypes.Count);
                    for (int i = 0; i < expectedTypes.Count; i++)
                    {
                        Assert.AreEqual(expectedTypes[i].Id, actualTypes[i].Id);
                        Assert.AreEqual(expectedTypes[i].Name, actualTypes[i].Name);
                    }
                }

                [Test]
                public void Tags_SetGet_ShouldWork()
                {
                    // Arrange
                    var model = new WeaponFormModel();
                    var expectedTags = new List<WeaponTagFormModel>
            {
                new WeaponTagFormModel { Id = 1, Name = "Tag1" },
                new WeaponTagFormModel { Id = 2, Name = "Tag2" }
            };

                    // Act
                    model.Tags = expectedTags.ToList();
                    var actualTags = model.Tags.ToList();

                    // Assert
                    Assert.AreEqual(expectedTags.Count, actualTags.Count);
                    for (int i = 0; i < expectedTags.Count; i++)
                    {
                        Assert.AreEqual(expectedTags[i].Id, actualTags[i].Id);
                        Assert.AreEqual(expectedTags[i].Name, actualTags[i].Name);
                    }
                }
            }

            [Test]
            public void Complexity_SetGet_ShouldWork()
            {
                // Arrange
                var model = new WeaponFormModel();
                int expectedComplexity = 5;

                // Act
                model.Complexity = expectedComplexity;
                int actualComplexity = model.Complexity;

                // Assert
                Assert.AreEqual(expectedComplexity, actualComplexity);
            }

            [Test]
            public void Rating_SetGet_ShouldWork()
            {
                // Arrange
                var model = new WeaponFormModel();
                int expectedRating = 4;

                // Act
                model.Rating = expectedRating;
                int actualRating = model.Rating;

                // Assert
                Assert.AreEqual(expectedRating, actualRating);
            }

            [Test]
            public void TypeId_SetGet_ShouldWork()
            {
                // Arrange
                var model = new WeaponFormModel();
                int expectedTypeId = 1;

                // Act
                model.TypeId = expectedTypeId;
                int actualTypeId = model.TypeId;

                // Assert
                Assert.AreEqual(expectedTypeId, actualTypeId);
            }

            [Test]
            public void EngineerId_SetGet_ShouldWork()
            {
                // Arrange
                var model = new WeaponFormModel();
                Guid expectedEngineerId = Guid.NewGuid();

                // Act
                model.EngineerId = expectedEngineerId;
                Guid actualEngineerId = model.EngineerId;

                // Assert
                Assert.AreEqual(expectedEngineerId, actualEngineerId);
            }

            [Test]
            public void Images_SetGet_ShouldWork()
            {
                // Arrange
                var model = new WeaponFormModel();
                var expectedImages = new List<Image>();

                // Act
                model.Images = expectedImages;
                var actualImages = model.Images;

                // Assert
                Assert.AreSame(expectedImages, actualImages);
            }

            [Test]
            public void Constructor_InitializeCollections_ShouldWork()
            {
                // Arrange & Act
                var model = new WeaponFormModel();

                // Assert
                Assert.IsNotNull(model.Types);
                Assert.IsNotNull(model.Tags);
                // Add more assertions if needed
            }
        }
    }


}


