namespace PawsitivelyCare.DTOs.Comment
{
    public class CreateCommentDto
    {
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid PostId { get; set; }
        public Guid SenderId { get; set; }
    }
}
