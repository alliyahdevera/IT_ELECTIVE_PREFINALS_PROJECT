namespace HelpDesk.Models.ViewModels
{
    public class TeamIndexViewModel
    {
        public string TeamName { get; set; } = string.Empty;
        public string DepartmentName { get; set; } = string.Empty;
        public List<string> Members { get; set; } = new();
    }
}
