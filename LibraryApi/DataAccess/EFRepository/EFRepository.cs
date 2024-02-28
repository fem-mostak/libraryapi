using LibraryApi.DataAccess.Interface;
using LibraryApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryApi.DataAccess.EFRepository
{
    public class EFRepository<T> : IRepository<T> where T : BaseDBEntity, new()
    {
        protected DataContext _dataContext;
        protected DbSet<T> _dbSet;

        public EFRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
            _dbSet = _dataContext.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T?> GetById(int TID)
        {
            return await _dbSet.FindAsync(TID);
        }

        public async Task Insert(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _dataContext.SaveChangesAsync();
        }

        public async Task Update(T entity)
        {
            _dataContext.Entry(entity).State = EntityState.Modified;
            await _dataContext.SaveChangesAsync();
        }

        public async Task Delete(int TID)
        {
            //First, fetch the Employee details based on the EmployeeID id
            var entity = _dbSet.Find(TID);
            //If the employee object is not null, then remove the employee
            if (entity != null)
            {
                //This will mark the Entity State as Deleted
                _dbSet.Remove(entity);
            }
        }
    }
}
