namespace Taarafou.Posts
{
    public class Post
    {
        public int    Id        { get; set; }
        public string Title     { get; set; } = string.Empty;
        public string Body      { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
