using HelpDesk.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Controllers
{
    public class CustomersController : Controller
    {
        private HelpDeskContext _context;

        public CustomersController(HelpDeskContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var customers = await _context.Customers
                .OrderBy(c => c.CompanyName)
                .ToListAsync();

            return View(customers);
        }
    }
}