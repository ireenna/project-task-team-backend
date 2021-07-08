using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using projectStructure.Common.DTOapp;
using projectStructure.DAL;
using projectStructure.DAL.Repositories;

namespace projectStructure.BLL.Services
{
    public sealed class ProjectService : BaseService
    {
        private readonly IRepository<Project> _projRepo;
        private readonly IRepository<Team> _teamRepo;
        private readonly IRepository<User> _userRepo;
        public ProjectService(IMapper mapper, IRepository<Project> projRepo, IRepository<Team> teamRepo, IRepository<User> userRepo) : base(mapper) {
            _projRepo = projRepo;
            _teamRepo = teamRepo;
            _userRepo = userRepo;
        }

        public IEnumerable<Project> GetAllProjects()
        {
            return _projRepo.Get();
        }
        public Project GetProject(int id)
        {            
            return _projRepo.GetByID(id);
        }
        public bool Create(ProjectCreateDTO proj)
        {
            try
            {
                var project = new Project {
                    Author = _userRepo.GetByID(proj.AuthorId),
                    CreatedAt = DateTime.Now,
                    Deadline = proj.Deadline,
                    Description = proj.Description,
                    Name = proj.Name,
                    Team = _teamRepo.GetByID(proj.TeamId)
                };
                _projRepo.Insert(project);
                _projRepo.SaveChanges();
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
                var oldProject = _projRepo.GetByID(id);
                oldProject.Deadline = proj.Deadline;
                oldProject.Description = proj.Description;
                oldProject.Name = proj.Name;
                oldProject.Team = _teamRepo.GetByID(proj.TeamId);
                _projRepo.Update(oldProject);
                _projRepo.SaveChanges();
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
                _projRepo.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }
    }
}
