using LibraryApi.Models;
using LibraryApi.DTO;

namespace LibraryApi.DataAccess.Interface
{
    public interface IAuthorRepository : IRepository<Author>
    {
        Task<bool> IsUniqueAuthor(string Name, DateTime DateOfBirth);
    }
}
