using BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : Controller
    {
        public HomeController()
        {
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
    }
}
