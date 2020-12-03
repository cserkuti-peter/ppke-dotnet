using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProjectManagementWebApp.Services;
using ProjectManagementWebApp.ViewModels;
using System.Threading.Tasks;

namespace ProjectManagementRestApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly ProjectAppService _projectAppService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, ProjectAppService projectAppService)
        {
            _logger = logger;
            _projectAppService = projectAppService;
        }

        [HttpGet]
        public async Task<ProjectDetailsViewModel> Get(int id)
        {
            return await this._projectAppService.GetProjectViewModelForDetailsAsync(id);
        }
    }
}
