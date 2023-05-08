using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using assignment4.Data;
using assignment4.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace assignment4.Pages.Employees
{
    public class CreateModel : PageModel
    {
        private readonly assignment4.Data.northwindContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CreateModel(assignment4.Data.northwindContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult OnGet()
        {
            var e = _context.Employees
            .Where(e => e.FirstName != null && e.LastName != null)
            .Select(e => new { e.EmployeeId, Name = $"{e.FirstName} {e.LastName}" })
            .ToList();

            ViewData["ReportsTo"] = new SelectList(e, "EmployeeId", "Name");
            return Page();

        }


        [BindProperty]
        public Employee Employee { get; set; } = default!;

        [BindProperty]
        public IFormFile? Upload { get; set; }

        public SelectList EmployeesList { get; set; }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.Employees == null || Employee == null)
            {
                return Page();
            }

            if (Upload != null)
            {
                var fileName = Path.GetFileNameWithoutExtension(Upload.FileName) +
                               "_" + DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss-ffffff") +
                               Path.GetExtension(Upload.FileName);
                var file = Path.Combine(_webHostEnvironment.WebRootPath, "img", fileName);

                using (var fileStream = new FileStream(file, FileMode.Create))
                {
                    await Upload.CopyToAsync(fileStream);
                }

                Employee.PhotoPath = fileName;
            }

            _context.Employees.Add(Employee);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
