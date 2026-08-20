using HelpDesk.Data;
using HelpDesk.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Controllers
{
    public class ReportsController : Controller
    {
        private HelpDeskContext _context;

        public ReportsController(HelpDeskContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> EmployeeWorkload()
        {
            var employees = await _context.Employees
                .Include(e => e.Department)
                .Include(e => e.TicketAssignments)
                .ThenInclude(a => a.Ticket)
                .ThenInclude(t => t.Status)
                .Where(e => e.IsActive)
                .ToListAsync();

            var result = new List<EmployeeWorkloadViewModel>();

            foreach (var e in employees)
            {
                var vm = new EmployeeWorkloadViewModel();
                vm.EmployeeName = e.FirstName + " " + e.LastName;
                vm.DepartmentName = e.Department.Name;

                int count = 0;
                foreach (var a in e.TicketAssignments)
                {
                    if (a.UnassignedAt == null && a.Ticket.Status.IsClosed == false)
                    {
                        count = count + 1;
                    }
                }
                vm.UnresolvedTicketCount = count;

                result.Add(vm);
            }

            result = result.OrderBy(r => r.DepartmentName).ThenBy(r => r.EmployeeName).ToList();

            return View(result);
        }

       
    }
}