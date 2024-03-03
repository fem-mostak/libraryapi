using LibraryApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryApi.DataAccess.EntityTypeConfiguration
{
    public class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Name)
                .IsRequired()
                .HasMaxLength(128);

            builder.Property(a => a.DateOfBirth)
                .IsRequired()
                .HasColumnType("date")
                .HasDefaultValueSql("'1900-01-01'");

            builder.Property(a => a.Genre)
                .IsRequired();

            builder.Property(a => a.CreatedAt)
                .IsRequired();
               // .HasDefaultValueSql("CURDATE()");

            builder.HasMany(a => a.Books)
                .WithOne(b => b.Author)
                .HasForeignKey(b => b.AuthorId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(a => new { a.Name, a.DateOfBirth })
                .IsUnique()
                .HasName("UniqueAuthor");
        }
    }
}
