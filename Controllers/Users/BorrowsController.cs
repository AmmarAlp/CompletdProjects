using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibraryAPIs.Data;
using LibraryAPIs.Models;
using Microsoft.AspNetCore.Identity;
using LibraryAPIs.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;


namespace LibraryAPIs.Controllers.Identity
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowsController : ControllerBase
    {
        private readonly LibraryAPIsContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public BorrowsController(LibraryAPIsContext context ,UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: api/Borrows
        [Authorize(Roles = "Employee")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Borrow>>> GetBorrow()
        {
          if (_context.Borrow == null)
          {
              return NotFound();
          }
            return await _context.Borrow.ToListAsync();
        }

        // GET: api/Borrows/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Borrow>> GetBorrow(int id)
        {
          if (_context.Borrow == null)
          {
              return NotFound();
          }
            var borrow = await _context.Borrow.FindAsync(id);

            if (borrow == null)
            {
                return NotFound();
            }

            return borrow;
        }



        // PUT: api/BorrowBooks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Employee")]
        [HttpPut]
        public async Task<IActionResult> PutBorrowBook(int bookCopyId, Borrow borrowBook)
        {
            var selectedBookCopy = await _context.Borrow!.FirstOrDefaultAsync(b => b.BookCopyId == bookCopyId && b.IsReturned == false);


            var loggedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            borrowBook.ResponsibleBy = loggedInUserId;

            if (selectedBookCopy == null)
            {
                return BadRequest();
            }

            var bookCopy = await _context.BookCopy!.FindAsync(bookCopyId);

            if (borrowBook.ReturnTime > borrowBook.DueTime)
            {
                var delayDays = (int)(borrowBook.ReturnTime - borrowBook.DueTime).Value.TotalDays;


                borrowBook.Amount += delayDays * 10;

                if (!borrowBook.Condition)
                {
                    borrowBook.Amount += 500;
                }

            }
            else
            {
                borrowBook.Amount = 0;
            }


            bookCopy!.IsAvailable = true;
            _context.BookCopy.Update(bookCopy);

            var member = await _context.Member!.FindAsync(selectedBookCopy.MemberIds);


            member!.Amount += borrowBook.Amount;
            _context.Member.Update(member);
            

           

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BorrowExists(selectedBookCopy.Id))
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

        // POST: api/Borrows
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        
        [Authorize(Roles ="Employee")]
        [HttpPost]
        public async Task<ActionResult<Borrow>> PostBorrow(Borrow borrow, string userName)
        {

            if (_context.Borrow == null)
            {
                return Problem("Entity set 'LibraryAPIContext.Borrow' is null.");
            }

            var user = await _userManager.FindByNameAsync(userName);
            if (user== null)
            {
                return NotFound("User not found.");
            }

            borrow.MemberIds = user.Id;


            var loggedUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var loggedInEmployee = await _context.Employee.FirstOrDefaultAsync(e => e.Id == loggedUserId);


            if (loggedInEmployee == null)
            {
                return Unauthorized();
            }
            
            
            borrow.ResponsibleBy = loggedUserId;

            //var memmber = await _context.Member!.FirstOrDefaultAsync(m => m.ApplicationUser!.IdNumber.Equals(tcNo));
           

            //await _context.Employee.FirstOrDefaultAsync(e => e.Id == user.Id);
            var bookCopy = await _context.BookCopy!.FindAsync(borrow.BookCopyId);

            if (bookCopy ==null)
            {
                return NotFound("BookCopy not found.");
            }

            if (!bookCopy.IsAvailable)
            {
                return BadRequest("BookCopy is not available.");
            }
            bookCopy.IsAvailable = false;
            _context.BookCopy.Update(bookCopy);


            await _context.Borrow.AddAsync(borrow);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBorrow", new { id = borrow.Id }, borrow);
        }

        // DELETE: api/Borrows/5
        [Authorize(Roles = "Employee")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBorrow(int id)
        {
            if (_context.Borrow == null)
            {
                return NotFound();
            }
            var borrow = await _context.Borrow.FindAsync(id);
            if (borrow == null)
            {
                return NotFound();
            }

            _context.Borrow.Remove(borrow);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BorrowExists(int id)
        {
            return (_context.Borrow?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
