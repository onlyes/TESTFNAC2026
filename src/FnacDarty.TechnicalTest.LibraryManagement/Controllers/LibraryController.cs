using FnacDarty.TechnicalTest.LibraryManagement.Domain.Services;
using FnacDarty.TechnicalTest.LibraryManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace FnacDarty.TechnicalTest.LibraryManagement.Controllers
{
    [Route("api/library")]
    [ApiController]
    public class LibraryController : ControllerBase
    {

        private readonly ILibraryService _bookService;

        public LibraryController(ILibraryService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet("books")]
        public IActionResult GetAllBooks()
        {
            var books = _bookService.GetAllBooks();
            return Ok(books);
        }

        [HttpPost("add-book")]
        public IActionResult AddBook([FromForm] AddBookRequest request)
        {
            _bookService.AddBook(request.Title, request.Author);

            return Ok();
        }


        [HttpGet("customers-with-borrowed-books")]
        public IActionResult GetCustomersWhoBorrowedBooks(DateTime from, DateTime to)
        {
            var books = _bookService.GetCustomersWhoBorrowedBooks(from, to);
            return Ok(books);
        }
    }
}
