using Microsoft.AspNetCore.Mvc;
using EmployeeManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly AppDbContext _context;

        public EmployeeController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string searchString, string departmentFilter)
        {
            // Get all employees
            var employees = from e in _context.Employees select e;

            // Apply search filter
            if (!string.IsNullOrEmpty(searchString))
            {
                employees = employees.Where(e => e.FirstName.Contains(searchString) || e.LastName.Contains(searchString));
            }

            // Apply department filter
            if (!string.IsNullOrEmpty(departmentFilter))
            {
                employees = employees.Where(e => e.Department == departmentFilter);
            }

            // Populate ViewBag.Departments with unique departments
            ViewBag.Departments = await _context.Employees
                .Select(e => e.Department)
                .Distinct()
                .ToListAsync();

            // Return the filtered employees to the view
            return View(await employees.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .Include(e => e.EmploymentHistories) // Include employment history if needed
                .FirstOrDefaultAsync(m => m.Id == id);

            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Employee employee)
        {
            if (id != employee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.Id == id);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> AddEmploymentHistory(int employeeId)
        {
            var employee = await _context.Employees.FindAsync(employeeId);
            if (employee == null)
            {
                return NotFound();
            }

            var employmentHistory = new EmploymentHistory { EmployeeId = employeeId };
            return View(employmentHistory);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddEmploymentHistory(EmploymentHistory employmentHistory)
        {
            if (ModelState.IsValid)
            {
                // Add the employment history record to the database
                _context.EmploymentHistories.Add(employmentHistory);
                await _context.SaveChangesAsync();

                // Redirect to the employee's details page
                return RedirectToAction(nameof(Details), new { id = employmentHistory.EmployeeId });
            }

            // If the model is invalid, return to the form with validation errors
            return View(employmentHistory);
        }
    }
}