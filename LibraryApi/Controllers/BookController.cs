using LibraryApi.DataAccess.EFRepository;
using LibraryApi.DataAccess.Interface;
using LibraryApi.DTO;
using LibraryApi.Models;
using LibraryApi.TDO;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
                AuthorId = author.Id,
                PublicationYear = bookRequestProvider.PublicationYear,
                QuantityInLibrary = bookRequestProvider.QuantityInLibrary,
                CreatedAt = DateTime.Now
            };

            await _bookRepository.Insert(book);

            book.Author = null;

            return Ok(book);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookByID(int id)
        {
            var book = await _bookRepository.GetById(id);

            if (book == null)
            {
                return BadRequest(new { messge = "Книга не найдена" });
            }

            return Ok(book);
        }

        [HttpPut("updateBook{id}")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] PatchBookDto patchBookDto)
        {
            var book = await _bookRepository.GetById(id);
            
            if (book == null)
            {
                return BadRequest(new { messge = "Книга не найдена" });
            }

            var author = await _authorRepository.GetById(book.AuthorId);

            if (patchBookDto.IsFieldPresent(nameof(book.Name)))
            {
                if (patchBookDto.Name.Length > 256)
                {
                    return BadRequest(new { messge = "Очень длиное имя" });
                }

                bool ifBookUnique = await _bookRepository.IsUniqueBook(patchBookDto.Name, book.AuthorId, book.PublicationYear);

                if (!ifBookUnique)
                {
                    return BadRequest(new { messge = "Tакая книга существует" });
                }

                book.Name = patchBookDto.Name;
            }

            if (patchBookDto.IsFieldPresent(nameof(book.PublicationYear)))
            {

                if (patchBookDto.PublicationYear < author.DateOfBirth.Year)
                {
                    return BadRequest(new { messge = "Год публикации не может быть раньше рождения автора" });
                }

                bool ifBookUnique = await _bookRepository.IsUniqueBook(book.Name, book.AuthorId, patchBookDto.PublicationYear);

                if (!ifBookUnique)
                {
                    return BadRequest(new { messge = "Tакая книга существует" });
                }

                book.PublicationYear = patchBookDto.PublicationYear;
            }

            await _bookRepository.Update(book);

            return NoContent();
        }

        [HttpPut("getBook{id}")]
        public async Task<IActionResult> GetBook(int id)
        {
            var book = await _bookRepository.GetById(id);

            if (book == null)
            {
                return BadRequest(new { messge = "Книга не найдена" });
            }

            if (book.QuantityInLibrary == 0)
            {
                return BadRequest(new { messge = "Нет книг в ниличии" });
            }

            book.QuantityInLibrary--;

            await _bookRepository.Update(book);

            return NoContent();
        }

        [HttpPut("setBook{id}")]
        public async Task<IActionResult> SetBook(int id)
        {
            var book = await _bookRepository.GetById(id);

            if (book == null)
            {
                return BadRequest(new { messge = "Книга не найдена" });
            }

            book.QuantityInLibrary++;

            await _bookRepository.Update(book);

            return NoContent();
        }
    }
}
