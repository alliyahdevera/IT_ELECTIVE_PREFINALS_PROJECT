using HelpDesk.Data;
using HelpDesk.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HelpDeskMVC.Controllers
{
    public class EmployeesController : Controller
    {
        private HelpDeskContext _context;

        public EmployeesController(HelpDeskContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var employees = await _context.Employees
                .Include(e => e.Department)
                .OrderBy(e => e.LastName)
                .ToListAsync();

            var result = new List<EmployeeIndexViewModel>();

            foreach (var e in employees)
            {
                var vm = new EmployeeIndexViewModel();
                vm.FullName = e.FirstName + " " + e.LastName;
                vm.Email = e.Email;
                vm.JobTitle = e.JobTitle;
                vm.DepartmentName = e.Department.Name;
                vm.IsActive = e.IsActive;
                result.Add(vm);
            }

            return View(result);
        }
    }
}