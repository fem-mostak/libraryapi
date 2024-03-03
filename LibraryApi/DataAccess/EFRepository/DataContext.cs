//using LibraryApi.DataAccess.EntityTypeConfiguration;
using LibraryApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryApi.DataAccess.EFRepository
{
    public class DataContext : DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            //https://duongnt.com/datetime-net6-postgresql/
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

           //Database.EnsureDeleted();
           Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => new { e.Name, e.DateOfBirth }).IsUnique();
                entity.Property(e => e.Name).HasMaxLength(128);
                entity.Property(e => e.Genre).HasMaxLength(128);
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => new { e.Name, e.AuthorId, e.PublicationYear }).IsUnique();
                entity.Property(e => e.Name).HasMaxLength(256);
            });
        }

    }
}
