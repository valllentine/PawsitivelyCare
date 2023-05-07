namespace PawsitivelyCare.DTOs.Post
{
    public class CreatePostDto
    {
        public string Title { get; set; }
        public string Content { get; set; } 
        public Guid PostTypeId { get; set; }
    }
}
