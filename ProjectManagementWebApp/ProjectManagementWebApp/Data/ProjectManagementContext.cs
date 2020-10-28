using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectManagementWebApp.Models;

namespace ProjectManagementWebApp.Data
{
    public class ProjectManagementContext : DbContext
    {
        public ProjectManagementContext (DbContextOptions<ProjectManagementContext> options)
            : base(options)
        {
        }

        public DbSet<ProjectManagementWebApp.Models.Project> Project { get; set; }
    }
}
