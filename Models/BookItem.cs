namespace BookAPI.Models
{
    public partial class BookItem
    {
        public long Id { get; set; }

        public string? Title { get; set; }

        public string? Author { get; set; }

        public string? Genre { get; set; }

        public int? PublicationYear { get; set; }

        public string? Publisher { get; set; }

        public string? Isbn { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        //public bool IsRead { get; internal set; }
    }

}
