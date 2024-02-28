using System.ComponentModel.DataAnnotations;

namespace LibraryApi.Models
{
    public class Author : BaseDBEntity
    {
        /// <summary>
        /// Имя автора
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Дата рождения автора
        /// </summary>
        public DateTime DateOfBirth { get; set; }
        /// <summary>
        /// Жанр
        /// </summary>
        public string Genre { get; set; }
        /// <summary>
        /// Дата регистрации в базе
        /// </summary>
        [Required]
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// книги автора
        /// </summary>
        public ICollection<Book> Books { get; set; }
    }
}
