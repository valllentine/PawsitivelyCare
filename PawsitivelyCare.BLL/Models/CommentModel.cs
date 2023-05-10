namespace PawsitivelyCare.BLL.Models
{
    public interface CommentModel
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid PostId { get; set; }
        public Guid SenderId { get; set; }
    }
}
