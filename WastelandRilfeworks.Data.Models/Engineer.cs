namespace WastelandRilfeworks.Data.Models
{
    public class Engineer
    {
        public Guid Id { get; set; }

        public string Username { get; set; }

        public Guid UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public int Aprovement { get; set; }

    }
}
