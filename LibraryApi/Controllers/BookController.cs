using LibraryApi.DataAccess.EFRepository;
using LibraryApi.DataAccess.Interface;
using LibraryApi.DTO;
using LibraryApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Controllers
{
    [ApiController]
    [Route("api/book/[controller]")]
    public class BookController : ControllerBase
    {
        private IBookRepository _bookRepository;
        private IAuthorRepository _authorRepository;

        public BookController(IBookRepository bookRepository, IAuthorRepository authorRepository)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
        }

        [HttpGet("allBook")]
        public async Task<IActionResult> AllBook()
        {
            var books = await _bookRepository.GetAllAsync();

            return Ok(books);
        }

        [HttpPost("addBok")]
        public async Task<IActionResult> AddBook([FromBody] BookRequestDto bookRequestProvider)
        {

            var author = await _authorRepository.GetById(bookRequestProvider.AuthorId);

            if (author == null)
            {
                return BadRequest(new { messge = "Автор не найден" });
            }

            if (bookRequestProvider.Name.Length > 256)
            {
                return BadRequest(new { messge = "Очень длиное имя" });
            }

            if (bookRequestProvider.PublicationYear < author.DateOfBirth.Year)
            {
                return BadRequest(new { messge = "Год публикации не может быть раньше рождения автора" });
            }

            if (bookRequestProvider.QuantityInLibrary < 0)
            {
                return BadRequest(new { messge = "Не верно указано количество книг" });
            }

            bool ifBookUnique = await _bookRepository.IsUniqueBook(bookRequestProvider.Name, bookRequestProvider.AuthorId, bookRequestProvider.PublicationYear);

            if (!ifBookUnique)
            {
                return BadRequest(new { messge = "Tакая книга существует" });
            }

            var book = new Book()
            {
                Name = bookRequestProvider.Name,
                Author = author,
                //AuthorId = author.Id,
                PublicationYear = bookRequestProvider.PublicationYear,
                QuantityInLibrary = bookRequestProvider.QuantityInLibrary,
                CreatedAt = DateTime.Now
            };

            await _bookRepository.Insert(book);

            return Ok(book);
        }
    }
}
