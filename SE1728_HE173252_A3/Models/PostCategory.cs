namespace SE1728_HE173252_A3.Models
{
    public class PostCategory
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }

        public ICollection<Post> Posts { get; set; }
    }
}
