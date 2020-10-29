using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using ProjectManagementWebApp.Models;

namespace ProjectManagementWebApp.Data
{
    public class ProjectManagementContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>
    {
        public ProjectManagementContext(DbContextOptions<ProjectManagementContext> options)
            : base(options)
        {
        }

        public DbSet<Comment> Comments { get; set; }
        public DbSet<FileMeta> FileMetas { get; set; }
        public DbSet<Project> Project { get; set; }
        public DbSet<Task> Tasks { get; set; }
    }
}
