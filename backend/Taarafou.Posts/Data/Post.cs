namespace Taarafou.Posts.Data
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Body { get; set; } = null!;   // الحقل الجديد
    }
}
