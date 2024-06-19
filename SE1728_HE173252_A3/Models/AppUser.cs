namespace SE1728_HE173252_A3.Models
{
    public class AppUser
    {
        public int UserID { get; set; }
        public string Fullname { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public ICollection<Post> Posts { get; set; }
    }
}
