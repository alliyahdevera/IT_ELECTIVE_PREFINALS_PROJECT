using HelpDesk.Models;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Data
{
    public class HelpDeskContext : DbContext
    {
        public HelpDeskContext(DbContextOptions<HelpDeskContext> options) : base(options)
        {
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<TeamMember> TeamMembers { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<TicketCategory> TicketCategories { get; set; }
        public DbSet<TicketPriority> TicketPriorities { get; set; }
        public DbSet<TicketStatus> TicketStatuses { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketAssignment> TicketAssignments { get; set; }
        public DbSet<TicketComment> TicketComments { get; set; }
        public DbSet<TicketAttachment> TicketAttachments { get; set; }
        public DbSet<TicketTag> TicketTags { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Department>().HasIndex(d => d.Name).IsUnique();
            builder.Entity<Employee>().HasIndex(e => e.Email).IsUnique();
            builder.Entity<Tag>().HasIndex(t => t.Name).IsUnique();
            builder.Entity<TicketPriority>().HasIndex(p => p.Name).IsUnique();
            builder.Entity<TicketStatus>().HasIndex(s => s.Name).IsUnique();
            builder.Entity<Team>().HasIndex(t => new { t.DepartmentId, t.Name }).IsUnique();

            builder.Entity<TeamMember>().HasKey(tm => new { tm.TeamId, tm.EmployeeId });
            builder.Entity<TicketAssignment>().HasKey(ta => new { ta.TicketId, ta.EmployeeId });
            builder.Entity<TicketTag>().HasKey(tt => new { tt.TicketId, tt.TagId });

            builder.Entity<Employee>()
                .HasOne(e => e.Department)
                .WithMany(d => d.Employees)
                .HasForeignKey(e => e.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Team>()
                .HasOne(t => t.Department)
                .WithMany(d => d.Teams)
                .HasForeignKey(t => t.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<TeamMember>()
                .HasOne(tm => tm.Team)
                .WithMany(t => t.TeamMembers)
                .HasForeignKey(tm => tm.TeamId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<TeamMember>()
                .HasOne(tm => tm.Employee)
                .WithMany(e => e.TeamMembers)
                .HasForeignKey(tm => tm.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<TicketCategory>()
                .HasOne(c => c.ParentCategory)
                .WithMany(c => c.ChildCategories)
                .HasForeignKey(c => c.ParentCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Ticket>()
                .HasOne(t => t.Customer).WithMany(c => c.Tickets)
                .HasForeignKey(t => t.CustomerId).OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Ticket>()
                .HasOne(t => t.Category).WithMany(c => c.Tickets)
                .HasForeignKey(t => t.CategoryId).OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Ticket>()
                .HasOne(t => t.Priority).WithMany(p => p.Tickets)
                .HasForeignKey(t => t.PriorityId).OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Ticket>()
                .HasOne(t => t.Status).WithMany(s => s.Tickets)
                .HasForeignKey(t => t.StatusId).OnDelete(DeleteBehavior.Restrict);

            builder.Entity<TicketAssignment>()
                .HasOne(a => a.Ticket).WithMany(t => t.TicketAssignments)
                .HasForeignKey(a => a.TicketId).OnDelete(DeleteBehavior.Restrict);

            builder.Entity<TicketAssignment>()
                .HasOne(a => a.Employee).WithMany(e => e.TicketAssignments)
                .HasForeignKey(a => a.EmployeeId).OnDelete(DeleteBehavior.Restrict);

            builder.Entity<TicketComment>()
                .HasOne(c => c.Ticket).WithMany(t => t.TicketComments)
                .HasForeignKey(c => c.TicketId).OnDelete(DeleteBehavior.Restrict);

            builder.Entity<TicketComment>()
                .HasOne(c => c.Employee).WithMany(e => e.TicketComments)
                .HasForeignKey(c => c.EmployeeId).OnDelete(DeleteBehavior.Restrict);

            builder.Entity<TicketAttachment>()
                .HasOne(a => a.Ticket).WithMany(t => t.TicketAttachments)
                .HasForeignKey(a => a.TicketId).OnDelete(DeleteBehavior.Restrict);

            builder.Entity<TicketTag>()
                .HasOne(tt => tt.Ticket).WithMany(t => t.TicketTags)
                .HasForeignKey(tt => tt.TicketId).OnDelete(DeleteBehavior.Restrict);

            builder.Entity<TicketTag>()
                .HasOne(tt => tt.Tag).WithMany(t => t.TicketTags)
                .HasForeignKey(tt => tt.TagId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}