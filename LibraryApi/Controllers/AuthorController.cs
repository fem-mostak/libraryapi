using LibraryApi.DataAccess.EFRepository;
using LibraryApi.DataAccess.Interface;
using LibraryApi.Models;
using LibraryApi.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibraryApi.TDO;

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
        public async Task<IActionResult> AddAuthor([FromBody] AuthorRequestDto authorRequestProvider)
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
        public async Task<IActionResult> UpdateAuthor(int id, [FromBody] PatchAuthorDto patchAuthorDto)
        {
            var author = await _authorRepository.GetById(id);

            if (author == null) 
            {
                return BadRequest(new { messge = "Автор не найден" });
            }

            if (patchAuthorDto.IsFieldPresent(nameof(author.Name)))
            {
                if (patchAuthorDto.Name.Length > 128)
                {
                    return BadRequest(new { messge = "Очень длиное имя" });
                }

                bool ifAuthorUnique = await _authorRepository.IsUniqueAuthor(patchAuthorDto.Name, author.DateOfBirth);

                if (!ifAuthorUnique)
                {
                    return BadRequest(new { messge = "Tакой Автор существует" });
                }

                author.Name = patchAuthorDto.Name;  
            }

            if (patchAuthorDto.IsFieldPresent(nameof(author.Genre)))
            {
                author.Genre = patchAuthorDto.Genre;
            }

            if (patchAuthorDto.IsFieldPresent(nameof(author.DateOfBirth)))
            {

                if (patchAuthorDto.DateOfBirth < new DateTime(1900, 01, 01))
                {
                    return BadRequest(new { messge = "Не верная дата рождения" });
                }

                bool ifAuthorUnique = await _authorRepository.IsUniqueAuthor(author.Name, (DateTime)patchAuthorDto.DateOfBirth);

                if (!ifAuthorUnique)
                {
                    return BadRequest(new { messge = "Tакой Автор существует" });
                }

                author.DateOfBirth = (DateTime)patchAuthorDto.DateOfBirth;
            }

            await _authorRepository.Update(author);

            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuthorByID(int id)
        {
            var author = await _authorRepository.GetById(id);

            if (author == null)
            {
                return BadRequest(new { messge = "Автор не найден" });
            }

            return Ok(author);
        }

    }
}
