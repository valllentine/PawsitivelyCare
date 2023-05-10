using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PawsitivelyCare.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawsitivelyCare.DAL.EntityConfiguration
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();

            builder.Property(x => x.Text).IsRequired();
            builder.Property(x => x.SenderId).IsRequired();
            builder.Property(x => x.PostId).IsRequired();
            builder.Property(x => x.CreatedAt).IsRequired();

            builder.ToTable("Comments");
        }
    }
}
