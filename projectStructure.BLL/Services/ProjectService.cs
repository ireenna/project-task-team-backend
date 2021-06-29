using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using projectStructure.DAL;
using projectStructure.DAL.Models;

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
