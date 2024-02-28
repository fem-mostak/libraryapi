using LibraryApi.DataAccess.Interface;
using LibraryApi.Models;

namespace LibraryApi.DataAccess.EFRepository
{
    public class BookRepository : EFRepository<Book>, IBookRepository
    {
        public BookRepository(DataContext dataContext) : base(dataContext)
        {

        }
    }
}
