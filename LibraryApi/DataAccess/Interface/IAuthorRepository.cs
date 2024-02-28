using LibraryApi.Models;
using LibraryApi.Providers;

namespace LibraryApi.DataAccess.Interface
{
    public interface IAuthorRepository : IRepository<Author>
    {
        Task<bool> IsUniqueAuthor(string Name, DateTime DateOfBirth);
    }
}
