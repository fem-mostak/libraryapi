using LibraryApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApi.DataAccess.Interface
{
    public interface IRepository<T> where T :  BaseDBEntity, new()
    {
        /// <summary>
        /// Получить все элементы <T> в базе
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<T>> GetAllAsync();

        /// <summary>
        /// Получить элемент <T> из базы по ID
        /// </summary>
        /// <param name="TID">ID элемента</param>
        /// <returns></returns>
        Task<T?> GetById(int TID);

        /// <summary>
        /// Вставить объект в базу
        /// </summary>
        /// <param name="entity">объект</param>
        Task Insert(T entity);

        /// <summary>
        /// Обновить объект в базе
        /// </summary>
        /// <param name="entity">объект</param>
        Task Update(T entity);

        /// <summary>
        /// Удалить объект из базы
        /// </summary>
        /// <param name="entity">объект</param>
        Task Delete(int TID);
    }
}
