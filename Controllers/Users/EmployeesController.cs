using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibraryAPIs.Data;
using LibraryAPIs.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace LibraryAPIs.Controllers.Identity
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly LibraryAPIsContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
       
        public EmployeesController(LibraryAPIsContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
           
        }

        // GET: api/Employees
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployee()
        {
            if (_context.Employee == null)
            {
                return NotFound();
            }
            return await _context.Employee.ToListAsync();
        }

        // GET: api/Employees/5
        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(string id)
        {
            if (_context.Employee == null)
            {
                return NotFound();
            }
            var employee = await _context.Employee.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }

        //GET: api/Members oturum açmış kullanıcı bilgilerini döner
        [Authorize(Roles = "Employee")]
        [HttpGet("Self")]
        public async Task<ActionResult<Employee>> GetEmployeeSelf()
        {
            var loggedEmployeeName = User.FindFirstValue(ClaimTypes.Name);
            if (loggedEmployeeName == null)
            {
                return NotFound();
            }



            var employee = await _context.Employee.Include(m => m.ApplicationUser).FirstOrDefaultAsync(e => e.ApplicationUser.UserName == loggedEmployeeName);


            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }

        // PUT: api/Employees/5
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(string id, Employee employee, string? currentPassword = null)
        {
            var applicationUser = await _userManager.FindByIdAsync(id);
            if (id != employee.Id)
            {
                return BadRequest();
            }

            if (applicationUser == null)
            {
                return NotFound();
            }

            applicationUser.Name = employee.ApplicationUser!.Name;
            applicationUser.MiddleName = employee.ApplicationUser!.MiddleName;
            applicationUser.FamilyName = employee.ApplicationUser!.FamilyName;
            applicationUser.Adddress = employee.ApplicationUser!.Adddress;
            applicationUser.BirthDate = employee.ApplicationUser!.BirthDate;
            applicationUser.RegisterDate = employee.ApplicationUser!.RegisterDate;
            applicationUser.Status = employee.ApplicationUser!.Status;
            applicationUser.Email = employee.ApplicationUser!.Email;

            await _userManager.UpdateAsync(applicationUser);

            if (currentPassword != null)
            {
                await _userManager.ChangePasswordAsync(applicationUser, currentPassword, "NewPassword"); // Yeni şifreyi geçin
            }

            employee.ApplicationUser = null;
            _context.Entry(employee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
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

        // POST: api/Employees
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee(Employee employee, string password)
        {
            if (_context.Employee == null)
            {
                return Problem("Entity set 'LibraryAPIsContext.Employee'  is null.");
            }

            await _userManager.CreateAsync(employee.ApplicationUser!, password);
            await _userManager.AddToRoleAsync(employee.ApplicationUser!, "Employee");

            employee.Id = employee.ApplicationUser!.Id;
            employee.ApplicationUser = null;
            _context.Employee.Add(employee);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (EmployeeExists(employee.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetEmployee", new { id = employee.Id }, employee);
        }

        // DELETE: api/Employees/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(string id)
        {
            if (_context.Employee == null)
            {
                return NotFound();
            }
            var employee = await _context.Employee.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            _context.Employee.Remove(employee);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        
        
        

        private bool EmployeeExists(string id)
        {
            return (_context.Employee?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
