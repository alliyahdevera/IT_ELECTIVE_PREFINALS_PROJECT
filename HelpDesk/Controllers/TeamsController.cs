using HelpDesk.Data;
using HelpDesk.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Controllers
{
    public class TeamsController : Controller
    {
        private HelpDeskContext _context;

        public TeamsController(HelpDeskContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var teams = await _context.Teams
                .Include(t => t.Department)
                .Include(t => t.TeamMembers)
                .ThenInclude(tm => tm.Employee)
                .OrderBy(t => t.Name)
                .ToListAsync();

            var result = new List<TeamIndexViewModel>();

            foreach (var t in teams)
            {
                var vm = new TeamIndexViewModel();
                vm.TeamName = t.Name;
                vm.DepartmentName = t.Department.Name;
                vm.Members = new List<string>();

                foreach (var tm in t.TeamMembers)
                {
                    vm.Members.Add(tm.Employee.FirstName + " " + tm.Employee.LastName);
                }

                result.Add(vm);
            }

            return View(result);
        }
    }
}