using LibraryApi.Models;

namespace LibraryApi.Providers
{
    public class AuthorRequestProvider
    {
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

    }
}
