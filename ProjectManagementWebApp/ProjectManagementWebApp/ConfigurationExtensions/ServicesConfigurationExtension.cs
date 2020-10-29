using ProjectManagementWebApp.Services;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServicesConfigurationExtension
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();

            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<IApplicationUserService, ApplicationUserService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<ITaskService, TaskService>();

            return services;
        }
    }
}
