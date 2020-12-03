using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjectManagementWebApp.Models;

namespace ProjectManagementWebApp.Data
{
    public class ProjectManagementContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>
    {
        public ProjectManagementContext (DbContextOptions<ProjectManagementContext> options)
            : base(options)
        {
        }

        public DbSet<ProjectManagementWebApp.Models.Project> Project { get; set; }
        public DbSet<ProjectManagementWebApp.Models.TaskModel> Tasks { get; set; }
        public DbSet<ProjectManagementWebApp.Models.Comment> Comments { get; set; }
        public DbSet<ProjectManagementWebApp.Models.FileMeta> FileMetas { get; set; }


        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Project>().HasData(new Models.Project { 
        //    ID=1,
        //    Description = "seeded by onmodelcreating",
        //    Name= "seeded1"
        //    });
        //    base.OnModelCreating(modelBuilder);
        //}
    }
}
