using LibraryApi.Models;

namespace LibraryApi.DTO
{
    public class BookRequestDto
    {
        public int PublicationYear { get; set; }

        public string Name { get; set; }

        public int AuthorId { get; set; }

        public int QuantityInLibrary { get; set; }

    }
}
