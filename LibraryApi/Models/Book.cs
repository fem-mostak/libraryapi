using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;

namespace LibraryApi.Models
{
    public class Book : BaseDBEntity
    {
        public int PublicationYear { get; set; }

        public string Name { get; set; }

        public int AuthorId { get; set; }

        public int QuantityInLibrary { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public Author Author { get; set; }
    }
}
