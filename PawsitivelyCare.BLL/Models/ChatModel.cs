namespace PawsitivelyCare.BLL.Models
{
    public class ChatModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid PostId { get; set; }
        public Guid PostCreatorId { get; set; }
    }
}
