using BookAPI.Contexts;
using Microsoft.AspNetCore.Mvc;
using BookAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BookAPI.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly BookContext _context;

        public BookController(BookContext context)
        {
            _context = context;
        }

        // Get all books
        [HttpGet]
        public IActionResult GetAll()
        {
            var books = _context.BookItems.ToList();
            if (books.Count == 0)
            {
                return NotFound("No books found.");
            }

            return Ok(books);
        }

        // Get a single book by ID
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var book = _context.BookItems.FirstOrDefault(b => b.Id == id);

            if (book == null)
            {
                return NotFound($"Book with ID {id} not found.");
            }
            return Ok(book);
        }

        // Create a new book
        [HttpPost]
        public async Task<IActionResult> addBook([FromBody] BookItem newBook)
        {
            if (newBook == null)
            {
                return BadRequest("Book is null.");
            }

            await _context.BookItems.AddAsync(newBook);
            await _context.SaveChangesAsync();

           return Ok(newBook);
        }

        // Update an existing book
        [HttpPut("{id}")] // ez így jó
        public async Task<IActionResult> UpdateBook(int id, BookItem bookItem)
        {
            // Ellenőrizzük, hogy az ID-k egyeznek-e
            if (id != bookItem.Id)
            {
                return BadRequest("Book ID mismatch.");
            }

            // Beállítjuk a módosított entitás állapotát "Modified"-ra
            _context.Entry(bookItem).State = EntityState.Modified;

            try
            {
                // A változtatásokat mentjük
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                
                if (!BookItemExists(id))
                {
                    return NotFound("Book not found.");
                }
                else
                {
                    
                    throw;
                }
            }

            // Visszatérünk a sikeres művelet után
            return NoContent(); 
        }

        private bool BookItemExists(int id)
        {
            return _context.BookItems.Any(e => e.Id == id);
        }


        // Delete a book
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var book = _context.BookItems.FirstOrDefault(b => b.Id == id);

            if (book == null)
            {
                return NotFound($"Book with ID {id} not found.");
            }

            _context.BookItems.Remove(book);
            _context.SaveChanges();

            return Ok($"Book with ID {id} deleted.");
        }
    }
}
