using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryApi.Models
{
    [Index(nameof(Name), nameof(DateOfBirth), IsUnique = true)]
    public class Author : BaseDBEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(128)]
        public string Name { get; set; }

        /// <summary>
        /// Дата рождения автора
        /// </summary>
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime DateOfBirth { get; set; }


        /// <summary>
        /// Жанр
        /// </summary>
        [Required]
        [MaxLength(128)]
        public string Genre { get; set; }


        /// <summary>
        /// Дата регистрации в базе
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// книги автора
        /// </summary>
        public ICollection<Book> Books { get; set; }
    }
}
