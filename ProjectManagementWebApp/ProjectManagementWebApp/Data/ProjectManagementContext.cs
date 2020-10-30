using System;
using System.Linq;
using System.Threading;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using ProjectManagementWebApp.Models;
using ProjectManagementWebApp.Services;

namespace ProjectManagementWebApp.Data
{
    public class ProjectManagementContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>
    {
        private readonly ICurrentUserService currentUserService;

        public ProjectManagementContext(DbContextOptions<ProjectManagementContext> options, ICurrentUserService currentUserService)
            : base(options)
        {
            this.currentUserService = currentUserService;
        }

        public DbSet<Comment> Comments { get; set; }
        public DbSet<FileMeta> FileMetas { get; set; }
        public DbSet<Project> Project { get; set; }
        public DbSet<Task> Tasks { get; set; }

        public override int SaveChanges()
        {
            BeforeSaveChanges();

            return base.SaveChanges();
        }

        public override System.Threading.Tasks.Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            BeforeSaveChanges();

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            BeforeSaveChanges();

            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override System.Threading.Tasks.Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            BeforeSaveChanges();

            return base.SaveChangesAsync(cancellationToken);
        }

        protected virtual void BeforeSaveChanges()
        {
            if (!ChangeTracker.AutoDetectChangesEnabled)
            {
                ChangeTracker.DetectChanges();
            }

            var entries = ChangeTracker.Entries();

            var utcNow = DateTime.UtcNow;
            var filteredEntries = entries
                .Where(x => x.State != EntityState.Unchanged && x.State != EntityState.Detached)
                .Where(x => x.Entity is EntityBase);

            foreach (var entry in filteredEntries)
            {
                if (entry.State == EntityState.Added)
                {
                    var entityAsBase = entry.Entity as EntityBase;

                    entityAsBase.Created = utcNow;
                    entityAsBase.CreatedByUserId = currentUserService?.UserId;
                }
            }
        }
    }
}
