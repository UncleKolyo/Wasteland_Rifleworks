using NUnit.Framework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using WastelandRilfeworks.Data.Models;
using static WastelandGeneralConstants.WastelandGeneralConstants;

namespace YourNamespace.Tests.Models
{
    [TestFixture]
    public class ImageTests
    {
        [Test]
        public void Id_ShouldHaveKeyAttribute()
        {
            var idProperty = typeof(Image).GetProperty(nameof(Image.Id));

            var keyAttribute = idProperty.GetCustomAttributes(typeof(KeyAttribute), false)
                .FirstOrDefault() as KeyAttribute;

            Assert.NotNull(keyAttribute);
        }

        [Test]
        public void FileName_ShouldHaveRequiredAttribute()
        {
            var fileNameProperty = typeof(Image).GetProperty(nameof(Image.FileName));

            var requiredAttribute = fileNameProperty.GetCustomAttributes(typeof(RequiredAttribute), false)
                .FirstOrDefault() as RequiredAttribute;

            Assert.NotNull(requiredAttribute);
        }

        [Test]
        public void WeaponId_ShouldHaveForeignKeyAttribute()
        {
            var weaponIdProperty = typeof(Image).GetProperty(nameof(Image.WeaponId));

            var foreignKeyAttribute = weaponIdProperty.GetCustomAttributes(typeof(ForeignKeyAttribute), false)
                .FirstOrDefault() as ForeignKeyAttribute;

            Assert.NotNull(foreignKeyAttribute);
            Assert.AreEqual(nameof(Image.Weapon), foreignKeyAttribute.Name);
        }

        [Test]
        public void Weapon_ShouldBeNull()
        {
            var image = new Image();

            Assert.Null(image.Weapon);
        }


    }

    [TestFixture]
    public class SchematicTests
    {
        [Test]
        public void Id_ShouldHaveKeyAttribute()
        {
            var idProperty = typeof(Schematic).GetProperty(nameof(Schematic.Id));

            var keyAttribute = idProperty.GetCustomAttributes(typeof(KeyAttribute), false)
                .FirstOrDefault() as KeyAttribute;

            Assert.NotNull(keyAttribute);
        }

        [Test]
        public void FileName_ShouldHaveRequiredAttribute()
        {
            var fileNameProperty = typeof(Schematic).GetProperty(nameof(Schematic.FileName));

            var requiredAttribute = fileNameProperty.GetCustomAttributes(typeof(RequiredAttribute), false)
                .FirstOrDefault() as RequiredAttribute;

            Assert.NotNull(requiredAttribute);
        }

        [Test]
        public void FileName_ShouldHaveMaxLengthAttribute()
        {
            var fileNameProperty = typeof(Schematic).GetProperty(nameof(Schematic.FileName));

            var maxLengthAttribute = fileNameProperty.GetCustomAttributes(typeof(MaxLengthAttribute), false)
                .FirstOrDefault() as MaxLengthAttribute;

            Assert.NotNull(maxLengthAttribute);
            Assert.AreEqual(WastelandRilfeworks.Common.EntityValidationConstraints.Image.NameMaxLenght, maxLengthAttribute.Length);
        }

    }
    [TestFixture]
    public class UserReactionTests
    {
        [Test]
        public void Id_ShouldHaveIdProperty()
        {
            var idProperty = typeof(UserReaction).GetProperty(nameof(UserReaction.Id));
            Assert.NotNull(idProperty);
        }

        [Test]
        public void UserId_ShouldHaveUserIdProperty()
        {
            var userIdProperty = typeof(UserReaction).GetProperty(nameof(UserReaction.UserId));
            Assert.NotNull(userIdProperty);
        }

        [Test]
        public void WeaponId_ShouldHaveWeaponIdProperty()
        {
            var weaponIdProperty = typeof(UserReaction).GetProperty(nameof(UserReaction.WeaponId));
            Assert.NotNull(weaponIdProperty);
        }

        [Test]
        public void ReactionType_ShouldHaveReactionTypeProperty()
        {
            var reactionTypeProperty = typeof(UserReaction).GetProperty(nameof(UserReaction.ReactionType));
            Assert.NotNull(reactionTypeProperty);
        }

        [Test]
        public void ReactionType_ShouldBeEnumOfTypeReactionType()
        {
            var reactionTypeProperty = typeof(UserReaction).GetProperty(nameof(UserReaction.ReactionType));

            Assert.NotNull(reactionTypeProperty);
            Assert.True(reactionTypeProperty.PropertyType.IsEnum);
            Assert.AreEqual(typeof(ReactionType), reactionTypeProperty.PropertyType);
        }
    }
}