
namespace PawsitivelyCare.DAL.Entities
{
    public class PostCategory
    {
        public int Id { get; set; }
        public string Category { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}
