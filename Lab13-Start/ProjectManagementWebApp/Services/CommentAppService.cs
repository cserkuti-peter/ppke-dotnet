using AutoMapper;
using ProjectManagementWebApp.Data;
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
    }
}
