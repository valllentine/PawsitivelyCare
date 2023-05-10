namespace PawsitivelyCare.DAL.Entities
{
    public class Post
    {
        public enum PostType
        {
            Customer = 0,
            Executor = 1
        }

        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Location { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid CreatorId { get; set; }
        public PostType Type { get; set; }
        public int PostCategoryId { get; set; }

        public virtual User Creator { get; set; }
        public PostCategory PostCategory { get; set;}
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
