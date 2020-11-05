using ProjectManagementWebApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementWebApp.Models
{
    public static class DbInitializer
    {
        public static void Initialize(ProjectManagementContext context)
        {
            context.Database.EnsureCreated();

            if (context.Project.Any())
            {
                return;
            }

            var projects = new Project[]
            {
                new Project  { Name = "Project #1", Description = "Description #1"},
                new Project  { Name = "Project #2", Description = "Description #2"},
                new Project  { Name = "Project #3", Description = "Description #3"},
                new Project  { Name = "Project #4", Description = "Description #4"},
            };

            context.Project.AddRange(projects);
            context.SaveChanges();
        }
    }
}
