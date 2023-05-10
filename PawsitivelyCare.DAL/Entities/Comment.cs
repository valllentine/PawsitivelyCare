using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawsitivelyCare.DAL.Entities
{
    public class Comment
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid PostId { get; set; }
        public Guid SenderId { get; set; }

        public virtual Post Post { get; set; }
        public virtual User Sender { get; set; }
    }
}
