using LibraryApi.Models;

namespace LibraryApi.DataAccess.Interface
{
    public interface IBookRepository : IRepository<Book>
    {
        Task<bool> IsUniqueBook(string Name, int AuthorId, int PublicationYear);
    }
}
