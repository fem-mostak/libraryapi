using LibraryApi.Models;

namespace LibraryApi.DTO
{
    public class AuthorRequestDto
    {
        public string Name { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Genre { get; set; }


    }
}
