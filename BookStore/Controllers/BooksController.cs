using BookStore.Business.DTO;
using BookStore.Business.Interfaces;
using BookStore.Mapper;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BooksController : Controller
    {
        private readonly IBookManager bookManager;

        public BooksController(IBookManager bookManager)
        {
            this.bookManager = bookManager;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookViewModel>>> GetAllBookAsync()
        {
            var booksList = await bookManager.GetAllBooksAsync();
            var result = new List<BookViewModel>();
            foreach (var book in booksList)
            {
                result.Add(BookMapper.MapBook(book));
            }

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookViewModel>> GetBookAsync(int id)
        {
            var book = await bookManager.GetBookAsync(id);

            if (book == null)
            {
                return NotFound($"Couldn’t found Book of id {id}");
            }
            var bookView = BookMapper.MapBook(book);

            return bookView;
        }

        [HttpPost]
        public async Task<ActionResult<BookViewModel>> CreateBookAsync(BookCreateDTO newBook)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdBook = await bookManager.CreateBookAsync(newBook);
            BookViewModel bookView = BookMapper.MapBook(createdBook);
            return Ok(bookView);
        }

        [HttpPut]
        public async Task<ActionResult<BookViewModel>> UpdateBookAsync(BookUpdateDTO updateBook)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var modifiedBook = await bookManager.UpdateBookAsync(updateBook);

            if (modifiedBook == null)
            {
                return NotFound();
            }

            var bookView = BookMapper.MapBook(modifiedBook);
            return Ok(bookView);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<BookViewModel>> DeleteBookAsync(int id)
        {            
            await bookManager.DeleteBookAsync(id);
            return NoContent();
        }
    }
}
