namespace PawsitivelyCare.DAL.Entities
{
    public class Post
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid PostTypeId { get; set; }
        public Guid CreatorId { get; set; }

        public virtual PostType PostType { get; set; }
        public virtual User User { get; set; }
    }
}
