using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibraryAPIs.Data;
using LibraryAPIs.Model;
using LibraryAPIs.Models;
using System.Data;
using Microsoft.AspNetCore.Authorization;

namespace LibraryAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly LibraryAPIsContext _context;
        AuthorBook authorBook;
        LanguageBook languageBook;
        SubCategoryBook subCategoryBook;

        public BooksController(LibraryAPIsContext context)
        {
            _context = context;
        }

        // GET: api/Books
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBook()
        {
          if (_context.Book == null)
          {
              return NotFound();
          }
            return await _context.Book.ToListAsync();
        }

        // GET: api/Books/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
          if (_context.Book == null)
          {
              return NotFound();
          }
            var book = await _context.Book.FindAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            return book;
        }

        // PUT: api/Books/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Employee")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, Book book)
        {
            if (id != book.Id)
            {
                return BadRequest();
            }

            _context.Entry(book).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Books
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Employee")]
        [HttpPost]
        public async Task<ActionResult<Book>> PostBook(Book book)
        {
            
            if (_context.Book == null)
          {
              return Problem("Entity set 'LibraryAPIsContext.Book'  is null.");
          }
            _context.Book.Add(book);
            await _context.SaveChangesAsync();

            if (book.AuthorsIds!= null)
            {
                foreach (long authorId in book.AuthorsIds)
                {
                    authorBook = new AuthorBook();
                    authorBook.AuthorsId = authorId;
                    authorBook.BooksId = book.Id;
                    _context.AuthorBook.Add(authorBook);
                }

            }

            if (book.LanguagesCodes != null)
            {
                foreach (string languageCode in book.LanguagesCodes)
                {
                    languageBook = new LanguageBook();
                    languageBook.CodesId = languageCode;
                    languageBook.BooksId = book.Id;
                    _context.LanguageBook.Add(languageBook);
                }

            }


            if (book.SubCategoriesIds != null)
            {
                foreach (short subId in book.SubCategoriesIds)
                {
                    subCategoryBook = new SubCategoryBook();
                    subCategoryBook.SubId = subId;
                    subCategoryBook.BooksId = book.Id;
                    _context.SubCategoryBook.Add(subCategoryBook);
                }

            }

            return CreatedAtAction("GetBook", new { id = book.Id }, book);
        }

        // DELETE: api/Books/5
        [Authorize(Roles = "Employee")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            if (_context.Book == null)
            {
                return NotFound();
            }
            var book = await _context.Book.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            _context.Book.Remove(book);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BookExists(int id)
        {
            return (_context.Book?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
