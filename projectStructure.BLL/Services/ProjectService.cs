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
        private readonly BaseRepository<Project> _projRepo;
        private readonly BaseRepository<Team> _teamRepo;
        private readonly BaseRepository<User> _userRepo;
        private readonly BaseRepository<Tasks> _tasksRepo;
        public ProjectService(IMapper mapper, ProjectsDbContext context) : base(mapper, context) {
            _projRepo = new BaseRepository<Project>(context);
            _teamRepo = new BaseRepository<Team>(context);
            _userRepo = new BaseRepository<User>(context);
            _tasksRepo = new BaseRepository<Tasks>(context);
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
                _context.SaveChanges();
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
                _context.SaveChanges();
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
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }
    }
}
