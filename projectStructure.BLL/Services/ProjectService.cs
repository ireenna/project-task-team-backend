using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using projectStructure.Common.Models;
using projectStructure.DAL;

namespace projectStructure.BLL.Services
{
    public sealed class ProjectService : BaseService
    {
        private readonly ProjectRepository<Project> _projRepo;
        public ProjectService(IMapper mapper) : base(mapper) {
            _projRepo = new ProjectRepository<Project>();
        }

        public IEnumerable<Project> GetAllProjects()
        {
            return _projRepo.Get();
        }
    }
}
