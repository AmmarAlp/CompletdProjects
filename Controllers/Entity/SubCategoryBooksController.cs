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
    public class SubCategoryBooksController : ControllerBase
    {
        private readonly LibraryAPIsContext _context;

        public SubCategoryBooksController(LibraryAPIsContext context)
        {
            _context = context;
        }

        // GET: api/SubCategoryBooks
        [Authorize(Roles = "Employee")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubCategoryBook>>> GetSubCategoryBook()
        {
          if (_context.SubCategoryBook == null)
          {
              return NotFound();
          }
            return await _context.SubCategoryBook.ToListAsync();
        }

        // GET: api/SubCategoryBooks/5
        [Authorize(Roles = "Employee")]
        [HttpGet("{id}")]
        public async Task<ActionResult<SubCategoryBook>> GetSubCategoryBook(long id)
        {
          if (_context.SubCategoryBook == null)
          {
              return NotFound();
          }
            var subCategoryBook = await _context.SubCategoryBook.FindAsync(id);

            if (subCategoryBook == null)
            {
                return NotFound();
            }

            return subCategoryBook;
        }

        // PUT: api/SubCategoryBooks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Employee")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSubCategoryBook(long id, SubCategoryBook subCategoryBook)
        {
            if (id != subCategoryBook.SubId)
            {
                return BadRequest();
            }

            _context.Entry(subCategoryBook).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubCategoryBookExists(id))
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

        // POST: api/SubCategoryBooks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Employee")]
        [HttpPost]
        public async Task<ActionResult<SubCategoryBook>> PostSubCategoryBook(SubCategoryBook subCategoryBook)
        {
          if (_context.SubCategoryBook == null)
          {
              return Problem("Entity set 'LibraryAPIsContext.SubCategoryBook'  is null.");
          }
            _context.SubCategoryBook.Add(subCategoryBook);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SubCategoryBookExists(subCategoryBook.SubId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetSubCategoryBook", new { id = subCategoryBook.SubId }, subCategoryBook);
        }

        // DELETE: api/SubCategoryBooks/5
        [Authorize(Roles = "Employee")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubCategoryBook(long id)
        {
            if (_context.SubCategoryBook == null)
            {
                return NotFound();
            }
            var subCategoryBook = await _context.SubCategoryBook.FindAsync(id);
            if (subCategoryBook == null)
            {
                return NotFound();
            }

            _context.SubCategoryBook.Remove(subCategoryBook);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SubCategoryBookExists(long id)
        {
            return (_context.SubCategoryBook?.Any(e => e.SubId == id)).GetValueOrDefault();
        }
    }
}
