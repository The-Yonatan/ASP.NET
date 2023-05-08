using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using assignment4.Data;
using assignment4.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.ProjectModel;

namespace assignment4.Pages.Employees
{
    public class DetailsModel : PageModel
    {
        private readonly assignment4.Data.northwindContext _context;

        public DetailsModel(assignment4.Data.northwindContext context)
        {
            _context = context;
        }

        public Employee Employee { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Employees == null)
            {
                return NotFound();
            }

            var employees = await _context.Employees.FirstOrDefaultAsync(m => m.EmployeeId == id);
            if (employees == null)
            {
                return NotFound();
            }
            else
            {
                var e = await _context.Employees.Include(m => m.ReportsToNavigation).FirstOrDefaultAsync(m => m.EmployeeId == id);  
                Employee = e;
            }
            return Page();
        }
    }
}
