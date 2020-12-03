using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjectManagementWebApp.Data;
using ProjectManagementWebApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementWebApp.Services
{
    public class CommentAppService
    {
        private readonly ProjectManagementContext _dbContext;
        private readonly IMapper _mapper;
        private readonly CurrentUserService _currentUserService;

        public CommentAppService(ProjectManagementContext context, IMapper mapper, CurrentUserService currentUserService)
        {
            this._dbContext = context;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task CreateCommentAsync(CreateCommentViewModel commentViewModel)
        {
            _dbContext.Comments.Add(new Models.Comment { UserId = _currentUserService.UserId, Data = commentViewModel.Comment, TaskId = commentViewModel.TaskId });
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<CommentViewModel>> ListCommentsAsync(int taskId)
        {
            return (await _dbContext.Comments.Include(k => k.User).Where(t => t.TaskId == taskId).ToListAsync())
                .Select(s => new CommentViewModel { Id = s.Id, Comment = s.Data, Username = s.User.Name, TaskId = s.TaskId })
                .ToList();
        }

        public async Task<CommentViewModel> GetCommentAsync(int commentId)
        {
            var c = await _dbContext.Comments.Include(k=>k.User).SingleOrDefaultAsync(c => c.Id == commentId);
            return new CommentViewModel { Id = c.Id, Comment = c.Data, TaskId = c.TaskId };
        }

        public async Task<int> DeleteCommentAsync(int commentId)
        {
            var currentUserId = _currentUserService.UserId;
            var comment = await _dbContext.Comments.SingleOrDefaultAsync(c => c.Id == commentId);
            var taskId = comment.TaskId;
            if (comment.UserId == currentUserId)
            {
                _dbContext.Remove(comment);
                await _dbContext.SaveChangesAsync();
                return taskId;
            }
            else
            {
                throw new Exception("You can't delete someone else's comment.");
            }
        }
    }
}
