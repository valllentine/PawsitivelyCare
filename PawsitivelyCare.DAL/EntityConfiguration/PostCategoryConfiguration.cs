using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PawsitivelyCare.DAL.Entities;

namespace PawsitivelyCare.DAL.EntityConfiguration
{
    public class PostCategoryConfiguration : IEntityTypeConfiguration<PostCategory>
    {
        public void Configure(EntityTypeBuilder<PostCategory> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();

            builder.Property(x => x.Category).IsRequired().HasMaxLength(50);

            builder.ToTable("PostCategories");
        }
    }
}
