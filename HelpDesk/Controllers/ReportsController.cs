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

        public async Task<IActionResult> DepartmentWorkload()
        {
            var departments = await _context.Departments
                .Include(d => d.Employees)
                .ThenInclude(e => e.TicketAssignments)
                .ThenInclude(a => a.Ticket)
                .ThenInclude(t => t.Status)
                .ToListAsync();

            var result = new List<DepartmentWorkloadViewModel>();

            foreach (var d in departments)
            {
                var vm = new DepartmentWorkloadViewModel();
                vm.DepartmentName = d.Name;
                vm.EmployeeCount = d.Employees.Count;

                var ticketIds = new List<int>();
                foreach (var e in d.Employees)
                {
                    foreach (var a in e.TicketAssignments)
                    {
                        if (a.UnassignedAt == null && a.Ticket.Status.IsClosed == false)
                        {
                            if (ticketIds.Contains(a.TicketId) == false)
                            {
                                ticketIds.Add(a.TicketId);
                            }
                        }
                    }
                }
                vm.UnresolvedTicketCount = ticketIds.Count;

                result.Add(vm);
            }

            result = result.OrderBy(r => r.DepartmentName).ToList();

            return View(result);
        }

        public async Task<IActionResult> UnassignedTickets()
        {
            var tickets = await _context.Tickets
                .Include(t => t.Customer)
                .Include(t => t.Priority)
                .Include(t => t.Status)
                .Include(t => t.TicketAssignments)
                .ToListAsync();

            var result = new List<UnassignedTicketViewModel>();

            foreach (var t in tickets)
            {
                bool hasActiveAssignment = false;
                foreach (var a in t.TicketAssignments)
                {
                    if (a.UnassignedAt == null)
                    {
                        hasActiveAssignment = true;
                    }
                }

                if (hasActiveAssignment == false)
                {
                    var vm = new UnassignedTicketViewModel();
                    vm.TicketId = t.Id;
                    vm.Subject = t.Subject;
                    vm.CustomerCompany = t.Customer.CompanyName;
                    vm.PriorityName = t.Priority.Name;
                    vm.StatusName = t.Status.Name;
                    vm.CreatedAt = t.CreatedAt;
                    result.Add(vm);
                }
            }

            result = result.OrderBy(r => r.CreatedAt).ToList();

            return View(result);
        }

        public async Task<IActionResult> MultipleAssignees()
        {
            var tickets = await _context.Tickets
                .Include(t => t.TicketAssignments)
                .ThenInclude(a => a.Employee)
                .ToListAsync();

            var result = new List<MultiAssigneeTicketViewModel>();

            foreach (var t in tickets)
            {
                var activeAssignments = new List<HelpDesk.Models.TicketAssignment>();
                foreach (var a in t.TicketAssignments)
                {
                    if (a.UnassignedAt == null)
                    {
                        activeAssignments.Add(a);
                    }
                }

                if (activeAssignments.Count > 1)
                {
                    var vm = new MultiAssigneeTicketViewModel();
                    vm.TicketId = t.Id;
                    vm.Subject = t.Subject;
                    vm.ActiveAssigneeCount = activeAssignments.Count;

                    var names = new List<string>();
                    foreach (var a in activeAssignments)
                    {
                        names.Add(a.Employee.FirstName + " " + a.Employee.LastName);
                    }
                    vm.Assignees = string.Join(", ", names);

                    result.Add(vm);
                }
            }

            result = result.OrderBy(r => r.TicketId).ToList();

            return View(result);
        }

    }
}