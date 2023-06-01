namespace PawsitivelyCare.DTOs.Comment
{
    public class CreateCommentDto
    {
        public string Text { get; set; }
        public Guid PostId { get; set; }
    }
}
