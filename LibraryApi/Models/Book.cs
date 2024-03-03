using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryApi.Models
{
    [Index(nameof(Name), nameof(AuthorId), nameof(PublicationYear), IsUnique = true)]
    public class Book : BaseDBEntity
    {

        [Required]
        [Range(1900, int.MaxValue)]
        public int PublicationYear { get; set; }

        [Required]
        [MaxLength(256)]
        public string Name { get; set; }

        [Required]
        public int AuthorId { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int QuantityInLibrary { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public DateTime CreatedAt { get; set; }

        [ForeignKey("AuthorId")]
        public Author Author { get; set; }
    }
}
