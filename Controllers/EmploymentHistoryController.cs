using Microsoft.AspNetCore.Mvc;
using EmployeeManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.Controllers
{
    public class EmploymentHistoryController : Controller
    {
        private readonly AppDbContext _context;

        public EmploymentHistoryController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var histories = await _context.EmploymentHistories
                .Include(eh => eh.Employee)
                .ToListAsync();
            return View(histories);
        }

        public IActionResult Create()
        {
            ViewBag.Employees = _context.Employees.ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmploymentHistory employmentHistory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employmentHistory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Employees = _context.Employees.ToList();
            return View(employmentHistory);
        }

        // Add other actions (Edit, Details, Delete) as needed
    }
}