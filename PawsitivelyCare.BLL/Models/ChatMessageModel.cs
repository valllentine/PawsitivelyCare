using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawsitivelyCare.BLL.Models
{
    internal class ChatMessageModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
        public int ChatId { get; set; }
        public int SenderId { get; set; }
    }
}
