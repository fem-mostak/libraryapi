using LibraryApi.DTO;

namespace LibraryApi.TDO
{
    public class PatchAuthorDto : PatchDtoBase
    {
        public string? Name { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string? Genre { get; set; }

    }
}
