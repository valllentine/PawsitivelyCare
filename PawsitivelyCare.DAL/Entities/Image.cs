namespace PawsitivelyCare.DAL.Entities
{
    public class Image
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public Guid PostId { get; set; }

        public virtual Post Post { get; set; }
    }
}
