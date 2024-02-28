using LibraryApi.DataAccess.EntityTypeConfiguration;
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
            modelBuilder.ApplyConfiguration(new AuthorConfiguration());
            modelBuilder.ApplyConfiguration(new BookConfiguration());
        }

    }
}
