using BookStore.Business.Interfaces;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthorsController : Controller
    {
        private readonly IAuthorManager authorManager;

        public AuthorsController(IAuthorManager authorManager)
        {
            this.authorManager = authorManager;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorViewModel>>> GetAllAuthorsAsync()
        {
            var authorsList = await authorManager.GetAllAuthorsAsync();
            var result = new List<AuthorViewModel>();
            foreach (var author in authorsList)
            {
                result.Add(Mapper.Mapper.MapAuthor(author));
            }
            return Ok(result);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorViewModel>> GetAuthorAsync(int id)
        {
            var author = await authorManager.GetAuthorAsync(id);

            if (author == null)
            {
                return NotFound($"Couldn’t found Author of id {id}");
            }
            var authorView = Mapper.Mapper.MapAuthor(author);

            return authorView;
        }
    }
}
