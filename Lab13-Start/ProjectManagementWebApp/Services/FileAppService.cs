using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjectManagementWebApp.Data;
using ProjectManagementWebApp.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementWebApp.Services
{
    public class FileAppService
    {
        private readonly ProjectManagementContext _dbContext;
        private readonly IMapper _mapper;
        private readonly CurrentUserService _currentUserService;

        public FileAppService(ProjectManagementContext context, IMapper mapper, CurrentUserService currentUserService)
        {
            this._dbContext = context;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task UploadFileAsync(int taskId, string path)
        {
            _dbContext.FileMetas.Add(new Models.FileMeta { FileName = path, TaskId = taskId });
            await _dbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteFileAsync(int fileId)
        {
            var f = await _dbContext.FileMetas.SingleOrDefaultAsync(k => k.Id == fileId);
            var taskId = f.TaskId;
            _dbContext.FileMetas.Remove(f);
            await _dbContext.SaveChangesAsync();
            return taskId;
        }

        public async Task<List<FileListViewModel>> ListTaskFilesASync(int taskId)
        {
            return (await _dbContext.FileMetas.Where(t => t.TaskId == taskId).ToListAsync())
                .Select(s => new FileListViewModel { Id = s.Id, FileName = s.FileName })
                .ToList();
        }
    }
}
