using LibraryApi.DataAccess.EFRepository;
using LibraryApi.DataAccess.Interface;
using LibraryApi.Models;
using LibraryApi.Providers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorController : ControllerBase
    {

        private IAuthorRepository _authorRepository;


        public AuthorController(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        [HttpGet("allAuthor")]
        public async Task<IActionResult> AllAuthor()
        {
            var authors = await _authorRepository.GetAllAsync();

            return Ok(authors);
        }

        [HttpPost("addAuthor")]
        public async Task<IActionResult> AddAuthor([FromBody] AuthorRequestProvider authorRequestProvider)
        {
            if (authorRequestProvider.Name.Length > 128) 
            {
                return BadRequest(new { messge = "Очень длиное имя" });
            }

            if (authorRequestProvider.DateOfBirth.Date < new DateTime(1900,01,01))
            {
                return BadRequest(new { messge = "Не верная дата рождения" });
            }

            bool ifAuthorUnique = await _authorRepository.IsUniqueAuthor(authorRequestProvider.Name, authorRequestProvider.DateOfBirth);

            if (!ifAuthorUnique)
            {
                return BadRequest(new { messge = "Tакой Автор существует" });
            }

            var author = new Author()
            {
                Name = authorRequestProvider.Name,
                Genre = authorRequestProvider.Genre,
                DateOfBirth = authorRequestProvider.DateOfBirth.Date,
                CreatedAt = DateTime.Now
            };

            await _authorRepository.Insert(author);

            return Ok(author);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuthor(int id, [FromBody] string newName = null, DateTime? newDateOfBirth = null, string newGenre = null)
        {
            var author = await _authorRepository.GetById(id);

            if (author == null)
            {
                return BadRequest(new { messge = "Автор не найден" });
            }

            if (newName != null)
            {
                if (newName.Length > 128)
                {
                    return BadRequest(new { messge = "Очень длиное имя" });
                }

                author.Name = newName;  
            }

            if (newDateOfBirth != null)
            {

                if (newDateOfBirth < new DateTime(1900, 01, 01))
                {
                    return BadRequest(new { messge = "Не верная дата рождения" });
                }

                author.DateOfBirth = (DateTime)newDateOfBirth;
            }

            bool ifAuthorUnique = await _authorRepository.IsUniqueAuthor(author.Name, author.DateOfBirth);

            if (!ifAuthorUnique)
            {
                await _authorRepository.Update(author);
            }

            return Ok(author);
        }

    }
}
