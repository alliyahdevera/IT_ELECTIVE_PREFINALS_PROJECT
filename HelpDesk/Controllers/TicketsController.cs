using HelpDesk.Data;
using HelpDesk.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Controllers
{
    public class TicketsController : Controller
    {
        private HelpDeskContext _context;

        public TicketsController(HelpDeskContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var tickets = await _context.Tickets
                .Include(t => t.Customer)
                .Include(t => t.Category)
                .Include(t => t.Priority)
                .Include(t => t.Status)
                .OrderByDescending(t => t.CreatedAt)
                .ToListAsync();

            var result = new List<TicketIndexViewModel>();

            foreach (var t in tickets)
            {
                var vm = new TicketIndexViewModel();
                vm.Id = t.Id;
                vm.Subject = t.Subject;
                vm.CustomerCompany = t.Customer.CompanyName;
                vm.CategoryName = t.Category.Name;
                vm.PriorityName = t.Priority.Name;
                vm.StatusName = t.Status.Name;
                vm.CreatedAt = t.CreatedAt;
                vm.DueAt = t.DueAt;
                result.Add(vm);
            }

            return View(result);
        }

        public async Task<IActionResult> Details(int id)
        {
            var ticket = await _context.Tickets
                .Include(t => t.Customer)
                .Include(t => t.Category)
                .Include(t => t.Priority)
                .Include(t => t.Status)
                .Include(t => t.TicketAssignments)
                .ThenInclude(a => a.Employee)
                .Include(t => t.TicketComments)
                .ThenInclude(c => c.Employee)
                .Include(t => t.TicketTags)
                .ThenInclude(tt => tt.Tag)
                .Include(t => t.TicketAttachments)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }
    }
}