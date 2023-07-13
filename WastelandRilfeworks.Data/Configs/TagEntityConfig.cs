namespace WastelandRilfeworks.Data.Configs
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    public class TagEntityConfig : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.HasData(this.GenerateTags());
        }

        private Tag[] GenerateTags()
        {
            ICollection<Tag> tags = new HashSet<Tag>();

            Tag tag;

            tag = new Tag()
            {
                Id = 1,
                Name = "One Handed"
            };
            tags.Add(tag);

            tag = new Tag()
            {
                Id = 2,
                Name = "Two Handed"
            };
            tags.Add(tag);

            tag = new Tag()
            {
                Id = 3,
                Name = "Sharp"
            };
            tags.Add(tag);

            tag = new Tag()
            {
                Id = 4,
                Name = "Blunt"
            };
            tags.Add(tag);

            tag = new Tag()
            {
                Id = 5,
                Name = "Small"
            };
            tags.Add(tag);

            tag = new Tag()
            {
                Id = 6,
                Name = "Big"
            };
            tags.Add(tag);

            tag = new Tag()
            {
                Id = 7,
                Name = "Pistol"
            };
            tags.Add(tag);

            tag = new Tag()
            {
                Id = 8,
                Name = "Rifle"
            };
            tags.Add(tag);

            tag = new Tag()
            {
                Id = 9,
                Name = "SMG"
            };
            tags.Add(tag);

            tag = new Tag()
            {
                Id = 10,
                Name = "Heavy Weapon"
            };
            tags.Add(tag);

            tag = new Tag()
            {
                Id = 11,
                Name = "2 Welded Pipes"
            };
            tags.Add(tag);

            tag = new Tag()
            {
                Id = 12,
                Name = "Dear Lord, please don't blow up in my face!"
            };
            tags.Add(tag);

            tag = new Tag()
            {
                Id = 14,
                Name = "Work In Progress"
            };
            tags.Add(tag);

            tag = new Tag()
            {
                Id = 15,
                Name = "Feedback appreciated"
            };
            tags.Add(tag);

            return tags.ToArray();
        }
    }
}
