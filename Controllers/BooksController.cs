using BertilloLibraryNowAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BertilloLibraryNowAPI.Controllers
{
    [Route("api/v1/books")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private static List<Book> books = new List<Book>
        {
            new Book { Id = 1, Title = "Clean Code", Author = "Robert Martin", Genre = "Programming", Available = true, PublishedYear = 2008 },
            new Book { Id = 2, Title = "The Pragmatic Programmer", Author = "Andrew Hunt", Genre = "Programming", Available = true, PublishedYear = 1999 },
        };

        // GET /api/v1/books
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(new { status = "success", data = books, message = "Books retrieved." });
        }

        // GET /api/v1/books/{id}
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var book = books.FirstOrDefault(b => b.Id == id);
            if (book == null)
                return NotFound(new { status = "error", data = (object?)null, message = "Book not found." });

            return Ok(new { status = "success", data = book, message = "Book retrieved." });
        }

        // POST /api/v1/books
        [HttpPost]
        public IActionResult Create([FromBody] Book newBook)
        {
            newBook.Id = books.Count + 1;
            books.Add(newBook);
            return CreatedAtAction(nameof(GetById), new { id = newBook.Id },
                new { status = "success", data = newBook, message = "Book created." });
        }

        // PUT /api/v1/books/{id}
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Book updatedBook)
        {
            var book = books.FirstOrDefault(b => b.Id == id);
            if (book == null)
                return NotFound(new { status = "error", data = (object?)null, message = "Book not found." });

            book.Title = updatedBook.Title;
            book.Author = updatedBook.Author;
            book.Genre = updatedBook.Genre;
            book.Available = updatedBook.Available;
            book.PublishedYear = updatedBook.PublishedYear;

            return Ok(new { status = "success", data = book, message = "Book updated." });
        }

        // DELETE /api/v1/books/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var book = books.FirstOrDefault(b => b.Id == id);
            if (book == null)
                return NotFound(new { status = "error", data = (object?)null, message = "Book not found." });

            books.Remove(book);
            return Ok(new { status = "success", data = (object?)null, message = "Book deleted." });
        }

    }
}
