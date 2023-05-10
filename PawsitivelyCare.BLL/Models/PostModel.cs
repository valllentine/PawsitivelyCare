using static PawsitivelyCare.DAL.Entities.Post;

namespace PawsitivelyCare.BLL.Models
{
    public class PostModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Location { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid CreatorId { get; set; }
        public PostType Type { get; set; }
        public int PostCategoryId { get; set; }
    }
}
