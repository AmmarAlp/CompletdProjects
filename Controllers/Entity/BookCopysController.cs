using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibraryAPIs.Data;
using LibraryAPIs.Models;
using Microsoft.AspNetCore.Authorization;

namespace LibraryAPIs.Controllers.Entity
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookCopysController : ControllerBase
    {
        private readonly LibraryAPIsContext _context;

        public BookCopysController(LibraryAPIsContext context)
        {
            _context = context;
        }

        // GET: api/BookCopys
        [Authorize(Roles = "Employee")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookCopy>>> GetBookCopy()
        {
          if (_context.BookCopy == null)
          {
              return NotFound();
          }
            return await _context.BookCopy.ToListAsync();
        }

        // GET: api/BookCopys/5
        [Authorize(Roles = "Employee")]
        [HttpGet("{id}")]
        public async Task<ActionResult<BookCopy>> GetBookCopy(int id)
        {
          if (_context.BookCopy == null)
          {
              return NotFound();
          }
            var bookCopy = await _context.BookCopy.FindAsync(id);

            if (bookCopy == null)
            {
                return NotFound();
            }

            return bookCopy;
        }

        // PUT: api/BookCopys/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Employee")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBookCopy(int id, BookCopy bookCopy)
        {
            if (id != bookCopy.Id)
            {
                return BadRequest();
            }

            _context.Entry(bookCopy).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookCopyExists(id))
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

        // POST: api/BookCopys
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Employee")]
        [HttpPost]
        public async Task<ActionResult<BookCopy>> PostBookCopy(BookCopy bookCopy)
        {
          if (_context.BookCopy == null)
          {
              return Problem("Entity set 'LibraryAPIsContext.BookCopy'  is null.");
          }
            _context.BookCopy.Add(bookCopy);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBookCopy", new { id = bookCopy.Id }, bookCopy);
        }

        // DELETE: api/BookCopys/5
        [Authorize(Roles = "Employee")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookCopy(int id)
        {
            if (_context.BookCopy == null)
            {
                return NotFound();
            }
            var bookCopy = await _context.BookCopy.FindAsync(id);
            if (bookCopy == null)
            {
                return NotFound();
            }

            _context.BookCopy.Remove(bookCopy);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BookCopyExists(int id)
        {
            return (_context.BookCopy?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
