﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PawsitivelyCare.DAL.Entities;

namespace PawsitivelyCare.DAL.EntityConfiguration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();

            builder.Property(x => x.Name).IsRequired().HasMaxLength(20);
            builder.Property(x => x.Surname).IsRequired().HasMaxLength(20);
            builder.Property(x => x.Email).IsRequired().HasMaxLength(254);
            builder.Property(x => x.Phone).IsRequired();
            builder.Property(x => x.Gender).HasConversion<int>();
            builder.Property(x => x.PasswordHash).IsRequired();
            builder.Property(x => x.CreatedAt).IsRequired();

            builder.HasMany(x => x.Comments)
               .WithOne(w => w.Sender)
               .HasForeignKey(x => x.SenderId)
               .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(x => x.ChatMessages)
               .WithOne(w => w.Sender)
               .HasForeignKey(x => x.SenderId)
               .OnDelete(DeleteBehavior.NoAction);

            builder.ToTable("Users");
        }
    }
}
