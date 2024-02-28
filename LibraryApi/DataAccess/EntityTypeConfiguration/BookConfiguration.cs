using LibraryApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryApi.DataAccess.EntityTypeConfiguration
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Name)
                .IsRequired()
                .HasMaxLength(256);

            builder.Property(b => b.AuthorId)
                .IsRequired();

            builder.Property(b => b.PublicationYear)
                .IsRequired();

            builder.Property(b => b.QuantityInLibrary)
                .IsRequired()
                .HasDefaultValue(0);

            builder.Property(b => b.CreatedAt)
                .IsRequired();
               // .HasDefaultValueSql("CURDATE()");

            builder.HasIndex(b => new { b.Name, b.PublicationYear })
                .IsUnique()
                .HasName("UniqueBook");

            builder.HasOne(b => b.Author)
                .WithMany(a => a.Books)
                .HasForeignKey(b => b.AuthorId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
