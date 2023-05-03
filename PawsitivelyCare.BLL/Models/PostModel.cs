﻿namespace PawsitivelyCare.BLL.Models
{
    public class PostModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public int PostTypeId { get; set; }
    }
}
