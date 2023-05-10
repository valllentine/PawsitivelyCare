using static PawsitivelyCare.DAL.Entities.Post;

namespace PawsitivelyCare.DTOs.Post
{
    public class CreatePostDto
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Location { get; set; }
        public PostType Type { get; set; }
        public int PostCategoryId { get; set; }
    }
}
