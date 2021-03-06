﻿using BookStore.Business.DTO;
using BookStore.Business.Interfaces;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
                result.Add(Mapper.Mapper.MapBook(book));
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
            var bookView = Mapper.Mapper.MapBook(book);

            return bookView;
        }

        [HttpPost]
        public async Task<ActionResult<BookViewModel>> CreateBookAsync([FromBody] BookCreateDTO newBook)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdBook = await bookManager.CreateBookAsync(newBook);
            BookViewModel bookView = Mapper.Mapper.MapBook(createdBook);
            return Ok(bookView);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BookViewModel>> UpdateBookAsync(
            [Required][Range(1, int.MaxValue, ErrorMessage = "Incorrect book id")]int id,
            [FromBody] BookUpdateDTO updateBook)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var modifiedBook = await bookManager.UpdateBookAsync(id, updateBook);

            if (modifiedBook == null)
            {
                return NotFound();
            }

            var bookView = Mapper.Mapper.MapBook(modifiedBook);
            return Ok(bookView);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<BookViewModel>> DeleteBookAsync(int id)
        {            
            await bookManager.DeleteBookAsync(id);
            return NoContent();
        }

        [HttpPost("{id}/Buy")]
        public async Task<ActionResult<BuyResultViewModel>> BuyBook(
            [Required][Range(1, int.MaxValue, ErrorMessage = "Incorrect book id")] int id,
            [FromBody] int customerId  //noramlly would be determined from current authenticated user
            )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var purcheseResult = await bookManager.SellBook(id, customerId);
            var response = new BuyResultViewModel
            {
                BookId = purcheseResult.BookId,
                CustomerId = purcheseResult.CustomerId,
                CustomerNewWallet = purcheseResult.CustomerNewWallet,
                IsStillInStock = purcheseResult.IsStillInStock
            };
            return Ok(response);
        }
    }
}
