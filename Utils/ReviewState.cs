namespace LibraryApp.Utils
{
    public class ReviewState
    {
        public int ReviewId { get; set; }

        public int ProductCode { get; set; }

        public int UserId { get; set; }
        public UserState User { get; set; }
        public byte Rating { get; set; }

        public string? Comment { get; set; }
        public DateTime ReviewDate { get; set; }
    }
}