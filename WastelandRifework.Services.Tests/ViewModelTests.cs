namespace WastelandRifework.Services.Tests
{
    using WastelandRifleworks.Web.ViewModels.Home;
    using WastelandRifleworks.Web.ViewModels.User;
    using WastelandRifleworks.Web.ViewModels.Weapon;
    using WastelandRifleworks.Web.ViewModels.Weapon.Enums;

    public class ViewModelTests
    {
        [TestFixture]
        public class ErrorViewModelTests
        {
            [Test]
            public void ShowRequestId_WhenRequestIdIsNotNull_ShouldReturnTrue()
            {
                // Arrange
                var viewModel = new ErrorViewModel
                {
                    RequestId = "12345"
                };

                // Act
                bool showRequestId = viewModel.ShowRequestId;

                // Assert
                Assert.IsTrue(showRequestId);
            }

            [Test]
            public void ShowRequestId_WhenRequestIdIsNull_ShouldReturnFalse()
            {
                // Arrange
                var viewModel = new ErrorViewModel
                {
                    RequestId = null
                };

                // Act
                bool showRequestId = viewModel.ShowRequestId;

                // Assert
                Assert.IsFalse(showRequestId);
            }
        }

        [TestFixture]
        public class UserViewModelTests
        {
            [Test]
            public void UserViewModel_Properties_ShouldBeSetCorrectly()
            {
                // Arrange
                string expectedId = "123";
                string expectedEmail = "test@example.com";
                string expectedUsername = "testuser";
                int expectedAprovement = 1;

                // Act
                var viewModel = new UserViewModel
                {
                    Id = expectedId,
                    Email = expectedEmail,
                    Username = expectedUsername,
                    Aprovement = expectedAprovement
                };

                // Assert
                Assert.AreEqual(expectedId, viewModel.Id);
                Assert.AreEqual(expectedEmail, viewModel.Email);
                Assert.AreEqual(expectedUsername, viewModel.Username);
                Assert.AreEqual(expectedAprovement, viewModel.Aprovement);
            }
        }

        [TestFixture]
        public class AllWeaponViewModelTests
        {
            [Test]
            public void AllWeaponViewModel_Properties_ShouldBeSetCorrectly()
            {
                // Arrange
                int expectedId = 1;
                string expectedName = "Sample Weapon";
                string expectedEngineer = "EngineerName";
                int expectedRating = 4;
                string expectedType = "Assault Rifle";
                int expectedComplexity = 3;
                string expectedDescription = "Description";
                var expectedTagNames = new List<string> { "Tag1", "Tag2" };
                var expectedImagesPaths = new List<string> { "image1.jpg", "image2.jpg" };

                // Act
                var viewModel = new AllWeaponViewModel
                {
                    Id = expectedId,
                    Name = expectedName,
                    Engineer = expectedEngineer,
                    Rating = expectedRating,
                    Type = expectedType,
                    Complexity = expectedComplexity,
                    Description = expectedDescription,
                    TagNames = expectedTagNames,
                    ImagesPaths = expectedImagesPaths
                };

                // Assert
                Assert.AreEqual(expectedId, viewModel.Id);
                Assert.AreEqual(expectedName, viewModel.Name);
                Assert.AreEqual(expectedEngineer, viewModel.Engineer);
                Assert.AreEqual(expectedRating, viewModel.Rating);
                Assert.AreEqual(expectedType, viewModel.Type);
                Assert.AreEqual(expectedComplexity, viewModel.Complexity);
                Assert.AreEqual(expectedDescription, viewModel.Description);
                CollectionAssert.AreEqual(expectedTagNames, viewModel.TagNames);
                CollectionAssert.AreEqual(expectedImagesPaths, viewModel.ImagesPaths);
            }
        }

        [TestFixture]
        public class WeaponDetailsViewModelTests
        {
            [Test]
            public void WeaponDetailsViewModel_Properties_ShouldBeSetCorrectly()
            {
                // Arrange
                int expectedId = 1;
                string expectedName = "Sample Weapon";
                string expectedEngineer = "EngineerName";
                int expectedRating = 4;
                string expectedType = "Assault Rifle";
                int expectedComplexity = 3;
                string expectedShortDescription = "Short description";
                string expectedFullDescription = "Full description";
                string expectedSchematicPath = "schematic.jpg";
                var expectedTagNames = new List<string> { "Tag1", "Tag2" };
                var expectedImagesPaths = new List<string> { "image1.jpg", "image2.jpg" };
                bool expectedHasUserLiked = true;
                bool expectedHasUserDisliked = false;

                // Act
                var viewModel = new WeaponDetailsViewModel
                {
                    Id = expectedId,
                    Name = expectedName,
                    Engineer = expectedEngineer,
                    Rating = expectedRating,
                    Type = expectedType,
                    Complexity = expectedComplexity,
                    ShortDescription = expectedShortDescription,
                    FullDescription = expectedFullDescription,
                    SchematicPath = expectedSchematicPath,
                    TagNames = expectedTagNames,
                    ImagesPaths = expectedImagesPaths,
                    HasUserLiked = expectedHasUserLiked,
                    HasUserDisliked = expectedHasUserDisliked
                };

                // Assert
                Assert.AreEqual(expectedId, viewModel.Id);
                Assert.AreEqual(expectedName, viewModel.Name);
                Assert.AreEqual(expectedEngineer, viewModel.Engineer);
                Assert.AreEqual(expectedRating, viewModel.Rating);
                Assert.AreEqual(expectedType, viewModel.Type);
                Assert.AreEqual(expectedComplexity, viewModel.Complexity);
                Assert.AreEqual(expectedShortDescription, viewModel.ShortDescription);
                Assert.AreEqual(expectedFullDescription, viewModel.FullDescription);
                Assert.AreEqual(expectedSchematicPath, viewModel.SchematicPath);
                CollectionAssert.AreEqual(expectedTagNames, viewModel.TagNames);
                CollectionAssert.AreEqual(expectedImagesPaths, viewModel.ImagesPaths);
                Assert.AreEqual(expectedHasUserLiked, viewModel.HasUserLiked);
                Assert.AreEqual(expectedHasUserDisliked, viewModel.HasUserDisliked);
            }
        }

        [TestFixture]
        public class AllWeaponsFilteredAndPagedServiceModelTests
        {
            [Test]
            public void AllWeaponsFilteredAndPagedServiceModel_Properties_ShouldBeSetCorrectly()
            {
                // Arrange
                int expectedTotalWeaponsCount = 10;
                var expectedWeapons = new List<AllWeaponViewModel>
            {
                new AllWeaponViewModel { Id = 1, Name = "Weapon 1" },
                new AllWeaponViewModel { Id = 2, Name = "Weapon 2" }
            };

                // Act
                var viewModel = new AllWeaponsFilteredAndPagedServiceModel
                {
                    TotalWeaponsCount = expectedTotalWeaponsCount,
                    Weapons = expectedWeapons
                };

                // Assert
                Assert.AreEqual(expectedTotalWeaponsCount, viewModel.TotalWeaponsCount);
                CollectionAssert.AreEqual(expectedWeapons, viewModel.Weapons);
            }

            [Test]
            public void AllWeaponsFilteredAndPagedServiceModel_WeaponsInitializedAsHashSet_ShouldWork()
            {
                // Arrange
                var viewModel = new AllWeaponsFilteredAndPagedServiceModel();

                // Act
                bool isHashSet = viewModel.Weapons is HashSet<AllWeaponViewModel>;

                // Assert
                Assert.IsTrue(isHashSet);
            }
        }

        [TestFixture]
        public class AllWeaponsQueryModelTests
        {
            [Test]
            public void AllWeaponsQueryModel_Properties_ShouldBeSetCorrectly()
            {
                // Arrange
                string expectedType = "Assault Rifle";
                string expectedTag = "Sniper Rifle";
                string expectedSearchTerm = "example";
                WeaponSorting expectedSorting = WeaponSorting.MostComplex;
                int expectedCurrentPage = 1;
                int expectedTotalWeapons = 100;
                int expectedWeaponPerPage = 20;
                var expectedTypes = new List<string> { "Assault Rifle", "Sniper Rifle" };
                var expectedTags = new List<string> { "Tag1", "Tag2" };
                var expectedWeapons = new List<AllWeaponViewModel>
            {
                new AllWeaponViewModel { Id = 1, Name = "Weapon 1" },
                new AllWeaponViewModel { Id = 2, Name = "Weapon 2" }
            };

                // Act
                var viewModel = new AllWeaponsQueryModel
                {
                    Type = expectedType,
                    Tag = expectedTag,
                    SearchTerm = expectedSearchTerm,
                    Sorting = expectedSorting,
                    CurrentPage = expectedCurrentPage,
                    TotalWeapons = expectedTotalWeapons,
                    WeaponPerPage = expectedWeaponPerPage,
                    Types = expectedTypes,
                    Tags = expectedTags,
                    Weapons = expectedWeapons
                };

                // Assert
                Assert.AreEqual(expectedType, viewModel.Type);
                Assert.AreEqual(expectedTag, viewModel.Tag);
                Assert.AreEqual(expectedSearchTerm, viewModel.SearchTerm);
                Assert.AreEqual(expectedSorting, viewModel.Sorting);
                Assert.AreEqual(expectedCurrentPage, viewModel.CurrentPage);
                Assert.AreEqual(expectedTotalWeapons, viewModel.TotalWeapons);
                Assert.AreEqual(expectedWeaponPerPage, viewModel.WeaponPerPage);
                CollectionAssert.AreEqual(expectedTypes, viewModel.Types.ToList());
                CollectionAssert.AreEqual(expectedTags, viewModel.Tags.ToList());
                CollectionAssert.AreEqual(expectedWeapons, viewModel.Weapons.ToList());
            }

            [Test]
            public void AllWeaponsQueryModel_DefaultValues_ShouldBeSetCorrectly()
            {
                // Arrange
                var viewModel = new AllWeaponsQueryModel();

                // Act & Assert
                Assert.AreEqual(1, viewModel.CurrentPage);
                Assert.AreEqual(20, viewModel.WeaponPerPage);
                Assert.AreEqual(0, viewModel.TotalWeapons);
                Assert.IsNotNull(viewModel.Types);
                Assert.IsNotNull(viewModel.Tags);
                Assert.IsNotNull(viewModel.Weapons);
            }
        }

        [TestFixture]
        public class WeaponPreDeleteViewModelTests
        {
            [Test]
            public void WeaponPreDeleteViewModel_Properties_ShouldBeSetCorrectly()
            {
                // Arrange
                int expectedId = 1;
                string expectedName = "Sample Weapon";
                int expectedRating = 4;
                string expectedType = "Assault Rifle";
                int expectedComplexity = 3;
                string expectedShortDescription = "Short description";
                string expectedFullDescription = "Full description";
                var expectedTagNames = new List<string> { "Tag1", "Tag2" };
                var expectedImagesPaths = new List<string> { "image1.jpg", "image2.jpg" };

                // Act
                var viewModel = new WeaponPreDeleteViewModel
                {
                    Id = expectedId,
                    Name = expectedName,
                    Rating = expectedRating,
                    Type = expectedType,
                    Complexity = expectedComplexity,
                    ShortDescription = expectedShortDescription,
                    FullDescription = expectedFullDescription,
                    TagNames = expectedTagNames,
                    ImagesPaths = expectedImagesPaths
                };

                // Assert
                Assert.AreEqual(expectedId, viewModel.Id);
                Assert.AreEqual(expectedName, viewModel.Name);
                Assert.AreEqual(expectedRating, viewModel.Rating);
                Assert.AreEqual(expectedType, viewModel.Type);
                Assert.AreEqual(expectedComplexity, viewModel.Complexity);
                Assert.AreEqual(expectedShortDescription, viewModel.ShortDescription);
                Assert.AreEqual(expectedFullDescription, viewModel.FullDescription);
                CollectionAssert.AreEqual(expectedTagNames, viewModel.TagNames);
                CollectionAssert.AreEqual(expectedImagesPaths, viewModel.ImagesPaths);
            }
        }
    }
}

