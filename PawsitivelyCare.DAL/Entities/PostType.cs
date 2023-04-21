namespace PawsitivelyCare.DAL.Entities
{
    public class PostType
    {
        public int Id { get; set; }
        public string Type { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}
