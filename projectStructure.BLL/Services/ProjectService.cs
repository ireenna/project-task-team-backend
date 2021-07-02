using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using projectStructure.BLL.Models;
using projectStructure.Common.DTOapp;
using projectStructure.DAL;
using projectStructure.DAL.DAL;
using projectStructure.DAL.Repositories;

namespace projectStructure.BLL.Services
{
    public sealed class ProjectService : BaseService
    {
        private readonly ProjectRepository<ProjectDAL> _projRepo;
        public ProjectService(IMapper mapper) : base(mapper) {
            _projRepo = new ProjectRepository<ProjectDAL>();
        }

        public IEnumerable<ProjectDAL> GetAllProjects()
        {
            return _projRepo.Get();
        }
        public ProjectDAL GetProject(int id)
        {            
            return _projRepo.Get(id);
        }
        public bool Create(ProjectCreateDTO proj)
        {
            try
            {
                var project = _mapper.Map<ProjectDAL>(proj);
                _projRepo.Create(project);
                return true;
            }
            catch
            {
                return false;
            }
            
        }
        public bool Update(ProjectUpdateDTO proj, int id)
        {
            try
            {
                var oldProject = _projRepo.Get(id);
                oldProject.Deadline = proj.Deadline;
                oldProject.Description = proj.Description;
                oldProject.Name = proj.Name;
                oldProject.TeamId = proj.TeamId;
                _projRepo.Update(oldProject);
                return true;
            }
            catch
            {
                return false;
            }

        }
        public bool Delete(int id)
        {
            try
            {
                _projRepo.Delete(id);
                return true;
            }
            catch
            {
                return false;
            }

        }
    }
}
