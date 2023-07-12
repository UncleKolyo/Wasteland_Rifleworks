namespace WastelandRilfeworks.Common
{

    public class EntityValidationConstraints
    {

        public static class Type
        {
            public const int NameMinLenght = 5;
            public const int NameMaxLenght = 15;
        }

        public static class Weapon
        {
            public const int NameMinLenght = 5;
            public const int NameMaxLenght = 75;
            public const int MinRating = 0;
            public const int MaxRating = 100;
            public const int DescriptionMaxLenght = 5000;
            public const int DescriptionMinLenght = 30;
            public const int MinComplexity = 0;
            public const int MaxComplexity = 100;
        }
    }
}
