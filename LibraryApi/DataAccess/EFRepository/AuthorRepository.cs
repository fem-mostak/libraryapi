using LibraryApi.DataAccess.Interface;
using LibraryApi.Models;
using LibraryApi.DTO;
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

        public override async Task<IEnumerable<Author>> GetAllAsync()
        {
            return await _dbSet
                .Include(b => b.Books)
                .ToListAsync();
        }

        public override async Task<Author?> GetById(int TID)
        {
            return await _dbSet
                .Include(b => b.Books)
                .FirstOrDefaultAsync(b => b.Id == TID);
        }


    }
}
