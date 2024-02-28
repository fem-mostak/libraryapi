using LibraryApi.DataAccess.Interface;
using LibraryApi.Models;
using LibraryApi.Providers;
using Microsoft.EntityFrameworkCore;

namespace LibraryApi.DataAccess.EFRepository
{
    public class AuthorRepository : EFRepository<Author>, IAuthorRepository
    {
        public AuthorRepository(DataContext dataContext) : base(dataContext)
        {
 
        }

        public async Task<bool> IsUniqueAuthor(string Name, DateTime DateOfBirth)
        {
            var _author = await _dbSet.FirstOrDefaultAsync(p => p.Name.ToLower() == Name.ToLower() && p.DateOfBirth.Date == DateOfBirth.Date);

            if (_author == null)
            {
                return true;
            }

            return false;
        }

    }
}
