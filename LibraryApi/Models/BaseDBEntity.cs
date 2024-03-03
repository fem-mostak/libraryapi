using System.ComponentModel.DataAnnotations;

namespace LibraryApi.Models
{
    public class BaseDBEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
