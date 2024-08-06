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

namespace LibraryAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LanguageBooksController : ControllerBase
    {
        private readonly LibraryAPIsContext _context;

        public LanguageBooksController(LibraryAPIsContext context)
        {
            _context = context;
        }

        // GET: api/LanguageBooks
        [Authorize(Roles = "Employee")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LanguageBook>>> GetLanguageBook()
        {
          if (_context.LanguageBook == null)
          {
              return NotFound();
          }
            return await _context.LanguageBook.ToListAsync();
        }

        // GET: api/LanguageBooks/5
        [Authorize(Roles = "Employee")]
        [HttpGet("{id}")]
        public async Task<ActionResult<LanguageBook>> GetLanguageBook(long id)
        {
          if (_context.LanguageBook == null)
          {
              return NotFound();
          }
            var languageBook = await _context.LanguageBook.FindAsync(id);

            if (languageBook == null)
            {
                return NotFound();
            }

            return languageBook;
        }

        // PUT: api/LanguageBooks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Employee")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLanguageBook(int id, LanguageBook languageBook)
        {
            if (id != languageBook.BooksId)
            {
                return BadRequest();
            }

            _context.Entry(languageBook).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LanguageBookExists(id))
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

        [Authorize(Roles = "Employee")]
        [HttpPost]
        public async Task<ActionResult<LanguageBook>> PostLanguageBook(LanguageBook languageBook)
        {
          if (_context.LanguageBook == null)
          {
              return Problem("Entity set 'LibraryAPIsContext.LanguageBook'  is null.");
          }
            _context.LanguageBook.Add(languageBook);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (LanguageBookExists(languageBook.BooksId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetLanguageBook", new { id = languageBook.CodesId }, languageBook);
        }

        // DELETE: api/LanguageBooks/5
        [Authorize(Roles = "Employee")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLanguageBook(long id)
        {
            if (_context.LanguageBook == null)
            {
                return NotFound();
            }
            var languageBook = await _context.LanguageBook.FindAsync(id);
            if (languageBook == null)
            {
                return NotFound();
            }

            _context.LanguageBook.Remove(languageBook);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LanguageBookExists(int id)
        {
            return (_context.LanguageBook?.Any(e => e.BooksId == id)).GetValueOrDefault();
        }

        
    }
}
