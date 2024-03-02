namespace LibraryApi.DTO
{
    public class PatchBookDto : PatchDtoBase
    {
        public string? Name { get; set; }

        public int PublicationYear { get; set; }

    }
}
