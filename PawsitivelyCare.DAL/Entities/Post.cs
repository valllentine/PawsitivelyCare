namespace PawsitivelyCare.DAL.Entities
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public int PostTypeId { get; set; }

        public virtual PostType PostType { get; set; }
    }
}
