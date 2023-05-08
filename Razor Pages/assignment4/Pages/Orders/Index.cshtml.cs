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

namespace assignment4.Pages.Orders
{
    public class IndexModel : PageModel
    {
        private readonly assignment4.Data.northwindContext _context;

        public IndexModel(assignment4.Data.northwindContext context)
        {
            _context = context;
        }

        public IList<Order> Order { get; set; } = default!;

        public SelectList Employees { get; set; }
        [BindProperty(SupportsGet = true)]
        public int? Orders { get; set; }

        public async Task OnGetAsync(int id)
        {
            var x = _context.Employees
            .OrderBy(x => x.LastName)
            .Select(x => new { x.EmployeeId, Name = $"{x.FirstName} {x.LastName}" })
            .ToList();

            Employees = new SelectList(x, "EmployeeId", "Name");

            var orders = _context.Orders.Include(o => o.ShipViaNavigation).Include(o => o.Employee).Where(o => o.Freight >= 250);

            if (Orders != null)
            
            {
                orders = orders.Where(y => y.EmployeeId == Orders);
            }

            Order = await orders.AsNoTracking().ToListAsync();
            
        }
    }
}
