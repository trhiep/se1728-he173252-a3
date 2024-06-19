namespace SE1728_HE173252_A3.Models
{
    public class Post
    {
        public int PostID { get; set; }
        public int AuthorID { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int PublishStatus { get; set; }
        public int CategoryID { get; set; }

        public AppUser Author { get; set; }
        public PostCategory Category { get; set; }
    }
}
