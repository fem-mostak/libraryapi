using LibraryApi.DataAccess.Interface;
using LibraryApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryApi.DataAccess.EFRepository
{
    public class BookRepository : EFRepository<Book>, IBookRepository
    {
        public BookRepository(DataContext dataContext) : base(dataContext)
        {

        }

        public async Task<bool> IsUniqueBook(string Name, int AuthorId, int PublicationYear)
        {
            var _book = await _dbSet.FirstOrDefaultAsync(p => p.Name.ToLower() == Name.ToLower() && p.Author.Id == AuthorId && p.PublicationYear == PublicationYear);

            if (_book == null)
            {
                return true;
            }

            return false;
        }
    }
}
