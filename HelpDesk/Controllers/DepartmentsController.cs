using HelpDesk.Data;
using HelpDesk.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Controllers
{
    public class DepartmentsController : Controller
    {
        private HelpDeskContext _context;

        public DepartmentsController(HelpDeskContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var departments = await _context.Departments
                .Include(d => d.Employees)
                .OrderBy(d => d.Name)
                .ToListAsync();

            var result = new List<DepartmentIndexViewModel>();

            foreach (var d in departments)
            {
                var vm = new DepartmentIndexViewModel();
                vm.Name = d.Name;
                vm.Description = d.Description;
                vm.IsActive = d.IsActive;
                vm.EmployeeCount = d.Employees.Count;
                result.Add(vm);
            }

            return View(result);
        }
    }
}