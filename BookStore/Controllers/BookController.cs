﻿using BookStore.Business.DTO;
using BookStore.Business.Interfaces;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : Controller
    {
        private readonly IBookManager bookManager;

        public BookController(IBookManager bookManager)
        {
            this.bookManager = bookManager;
        }

        [HttpGet]
        public ActionResult<IEnumerable<BookViewModel>> Index()
        {
            var result = new List<BookViewModel>();
            BookViewModel book1 = new BookViewModel()
            {
                Author = "Jhon Doe",
                Name = "Basic book"
            };
            result.Add(book1);
            
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<BookViewModel>> CreateBookAsync(BookCreateDTO newBook)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdBook = await bookManager.CreateBookAsync(newBook);
            var bookView = new BookViewModel
            {
                Author = createdBook.Author.FirstName + " " + createdBook.Author.LastName,
                BookId = createdBook.BookId,
                Description = createdBook.Description,
                ImageUrl = createdBook.ImageUrl,
                IsInStock = createdBook.InStock > 0,
                Name = createdBook.Name,
                Price = createdBook.Price
            };
            return Ok(bookView);
        }

        [HttpPut]
        public async Task<ActionResult<BookViewModel>> UpdateBookAsync(BookUpdateDTO updateBook)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedBook = await bookManager.UpdateBookAsync(updateBook);
            var bookView = new BookViewModel
            {
                BookId = updateBook.BookId,
                Description = updatedBook.Description,
                ImageUrl = updatedBook.ImageUrl,
                IsInStock = updatedBook.InStock > 0,
                Name = updatedBook.Name,
                Price = updatedBook.Price,
                Author = updatedBook.Author.FirstName + " " + updatedBook.Author.LastName,
            };
            return Ok(bookView);
        }
    }
}
