using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PawsitivelyCare.DAL.Entities;

namespace PawsitivelyCare.DAL.EntityConfiguration
{
    public class ChatMessageConfiguration : IEntityTypeConfiguration<ChatMessage>
    {
        public void Configure(EntityTypeBuilder<ChatMessage> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever().IsRequired();

            builder.Property(x => x.Text).IsRequired();
            builder.Property(x => x.SenderId).IsRequired();
            builder.Property(x => x.ChatId).IsRequired();
            builder.Property(x => x.CreatedAt).IsRequired();

            builder.ToTable("ChatMessages");
        }
    }
}
