using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibraryAPIs.Data;
using LibraryAPIs.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace LibraryAPIs.Controllers.Identity
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly LibraryAPIsContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public MembersController(LibraryAPIsContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // GET: api/Members.
        [Authorize(Roles = "Employee, Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Member>>> GetMember()
        {
          if (_context.Member == null)
          {
              return NotFound();
          }
            return await _context.Member.ToListAsync();
        }

        // GET: api/Members/5
        [Authorize(Roles = "Employee, Admin")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Member>> GetMember(string id)
        {
          if (_context.Member == null)
          {
              return NotFound();
          }
            var member = await _context.Member.FindAsync(id);

            if (member == null)
            {
                return NotFound();
            }

            return member;
        }
        //GET: api/Members oturum açmış kullanıcı bilgilerini döner
        [Authorize(Roles = "Member")]
        [HttpGet("Self")]
        public async Task<ActionResult<Member>> GetMemberSelf()
        {
            var loggedMemberName = User.FindFirstValue(ClaimTypes.Name);
            if (loggedMemberName == null)
            {
                return NotFound();
            }

            
            
            var member = await _context.Member.Include(m => m.ApplicationUser).FirstOrDefaultAsync(e => e.ApplicationUser.UserName == loggedMemberName);
            

            if (member == null)
            {
                return NotFound();
            }

            return member;
        }

        // PUT: api/Members/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Employee, Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMember(string id, Member member, string? currentPassword=null)
        {
            ApplicationUser applicationUser = _userManager.FindByIdAsync(id).Result;
            if (id != member.Id)
            {
                return BadRequest();
            }

            applicationUser.Name = member.ApplicationUser!.Name;
            applicationUser.MiddleName = member.ApplicationUser!.MiddleName;
            applicationUser.FamilyName = member.ApplicationUser!.FamilyName;
            applicationUser.Adddress = member.ApplicationUser!.Adddress;
            applicationUser.BirthDate = member.ApplicationUser!.BirthDate;
            applicationUser.RegisterDate = member.ApplicationUser!.RegisterDate;
            applicationUser.Status = member.ApplicationUser!.Status;
            applicationUser.Email = member.ApplicationUser!.Email;

            _userManager.UpdateAsync(applicationUser).Wait();
            if (currentPassword != null)
            {
                _userManager.ChangePasswordAsync(applicationUser, currentPassword, applicationUser.Password).Wait();
            }

            member.ApplicationUser = null;

            _context.Entry(member).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MemberExists(id))
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

        // POST: api/Members
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Employee, Admin")]
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Member>> PostMember(Member member ,string password)
        {
          if (_context.Member == null)
          {
              return Problem("Entity set 'LibraryAPIsContext.Member'  is null.");
          }

            var result = await _userManager.CreateAsync(member.ApplicationUser!, password);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            // Kullanıcı oluşturulduktan sonra rol atama işlemi
            var roleResult = await _userManager.AddToRoleAsync(member.ApplicationUser!, "Member");
            if (!roleResult.Succeeded)
            {
                return BadRequest(roleResult.Errors);
            }
            member.Id = member.ApplicationUser!.Id;
            member.ApplicationUser = null;
            _context.Member.Add(member);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (MemberExists(member.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetMember", new { id = member.Id }, member);
        }

        // DELETE: api/Members/5
        [Authorize(Roles = "Employee ,Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMember(string id)
        {
            if (_context.Member == null)
            {
                return NotFound();
            }
            var member = await _context.Member.FindAsync(id);
            if (member == null)
            {
                return NotFound();
            }

            _context.Member.Remove(member);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        

        private bool MemberExists(string id)
        {
            return (_context.Member?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
