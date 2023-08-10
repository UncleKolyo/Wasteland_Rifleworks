namespace WastelandRilfeworks.Data.Models
{
    public class UserReaction
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string WeaponId { get; set; }
        public ReactionType ReactionType { get; set; }
    }
}

public enum ReactionType
{
    Like,
    Dislike
}