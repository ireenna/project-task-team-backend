using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using projectStructure.BLL.Interfaces;
using projectStructure.BLL.ModelsInfo;
using projectStructure.DAL;
using projectStructure.DAL.Repositories;

namespace projectStructure.BLL.Services
{
    public sealed class LinqService : BaseService, ILinqService
    {
        private readonly IRepository<Project> _projRepo;
        private readonly IRepository<User> _userRepo;
        private readonly IRepository<Team> _teamRepo;
        private readonly IRepository<Tasks> _taskRepo;
        public LinqService(IMapper mapper, IRepository<Project> projRepo, IRepository<User> userRepo, IRepository<Team> teamRepo, IRepository<Tasks> taskRepo) : base(mapper)
        {
            _projRepo = projRepo;
            _userRepo = userRepo;
            _teamRepo = teamRepo;
            _taskRepo = taskRepo;
        }
        public List<Project> GetFullProjects()
        {
            return _projRepo.Get().Select(x => new Project
            {
                Id = x.Id,
                Author = _userRepo.GetByID(x.AuthorId),
                CreatedAt = x.CreatedAt,
                Deadline = x.Deadline,
                Description = x.Description,
                Name = x.Name,
                Tasks = _taskRepo.Get(y => y.ProjectId == x.Id).Select(t => new Tasks()
                {
                    Id = t.Id,
                    CreatedAt = t.CreatedAt,
                    FinishedAt = t.FinishedAt,
                    Description = t.Description,
                    Name = t.Name,
                    State = t.State,
                    ProjectId = t.ProjectId,
                    PerformerId = t.PerformerId,
                    Performer = _userRepo.GetByID(t.PerformerId)
                }).ToList(),
                Team = _teamRepo.Get(t=>t.Id == x.TeamId).Select(t=> new Team() { 
                    CreatedAt = t.CreatedAt,
                    Name = t.Name,
                    Id = t.Id
                }).FirstOrDefault()
            }).ToList();
        }
        public Dictionary<Project, int> GetQuantityOfUserTasks(int id)
        {
            var info = GetFullProjects()
                .Where(x => x.Author?.Id == id)
                .ToDictionary(x => x, x => x.Tasks.Count());

            if (info.Count == 0)
            {
                if (_userRepo.GetByID(id) is null)
                    throw new ArgumentOutOfRangeException();
            }

            return info;
        }
        public List<Tasks> GetUserTasks(int id)
        {
            var info = GetFullProjects()
                .SelectMany(pr => pr.Tasks)
                .Where(x => x.Performer.Id == id && x.Name.Length < 45)
                .ToList();

            if (info.Count == 0)
            {
                if (_userRepo.GetByID(id) is null)
                    throw new ArgumentOutOfRangeException();
            }

            return info;
        }
        public List<(int id, string name)> GetUserFinishedTasks(int id)
        {
            var info = GetFullProjects()
                .SelectMany(p => p.Tasks)
                .Where(x => x.Performer.Id == id && x.FinishedAt <= DateTime.Now && x.FinishedAt >= new DateTime(2021, 1, 1))
                .Select(x => (id: x.Id, name: x.Name))
                .ToList();

            if (info.Count == 0)
            {
                if (_userRepo.GetByID(id) is null)
                    throw new ArgumentOutOfRangeException();
            }

            return info;
        }
        public List<(int id, string name, List<User> users)> GetSortedUsersTeams()
        {
            var info = GetFullProjects()
                .Select(x => new { 
                    Id = x.Team.Id,
                    Name = x.Team.Name, 
                    Team = x.Team.CreatedAt,
                    Participants = _userRepo.Get(u=>u.TeamId == x.TeamId)}).Distinct()
                .Where(x => x.Participants.All(x => x.BirthDay <= DateTime.Now.AddYears(-10)))
                .Select(x => (x.Id, x.Name, x.Participants.OrderByDescending(p => p.RegisteredAt).ToList())).ToList();

            return info;
        }
        public List<IGrouping<User, Tasks>> GetSortedUsersWithTasks()
        {
            var info = GetFullProjects()
                .SelectMany(p => p.Tasks)
                .OrderByDescending(t => t.Name.Length)
                .GroupBy(t => t.Performer)
                .OrderBy(x => x.Key.FirstName)
                .ToList();

            return info;
        }
        public UserTaskInfo GetUserTasksInfo(int id)
        {
            var info = GetFullProjects()
                .SelectMany(p => p.Tasks)
                .Where(x => x.Performer.Id == id)
                .GroupBy(x => x.Performer)
                .Where(x => x.Key.Id == id)
                .Select(x => new
                {
                    user = x.Key,
                    lastProject = _projRepo.Get().Where(p => p.Team.Participants.Contains(x.Key)).OrderByDescending(x => x.CreatedAt).FirstOrDefault(),
                    tasks = x
                }).Select(x => new UserTaskInfo()
                {
                    User = x.user,
                    LastProject = x.lastProject,
                    LastProjectTasksCount = x.lastProject?.Tasks.Count() ?? 0,
                    RejectedTasks = x.tasks.Where(x => x.FinishedAt == null | (int)x.State == 2).Count(),
                    TheLongestTask = x.tasks.OrderByDescending(x => (x.FinishedAt ?? DateTime.Now) - x.CreatedAt).FirstOrDefault()
                }).FirstOrDefault();

            if (info is null)
            {
                if (_userRepo.GetByID(id) is null)
                    throw new ArgumentOutOfRangeException();
            }


            return info;
        }
        public List<ProjectsInfo> GetProjectsInfo()
        {
            var info = GetFullProjects().Select(p => new ProjectsInfo()
            {
                Project = p,
                LongestTaskByDescr = p.Tasks?.OrderByDescending(t => t.Description.Length).FirstOrDefault(),
                ShortestTaskByName = p.Tasks?.OrderBy(t => t.Name.Length).FirstOrDefault(),
                UsersCount = p.Description.Length > 20 ^ p.Tasks?.Count < 3 ? p.Team.Participants?.Count : 0
            }).ToList();

            return info;
        }
        public List<Tasks> GetUserUnfinishedTasks(int id)
        {
            var info = GetFullProjects().SelectMany(p => p.Tasks).Where(t => t.Performer.Id == id && t.State != TaskState.Done).ToList();
            if (info.Count == 0)
            {
                if (_userRepo.GetByID(id) is null)
                    throw new ArgumentOutOfRangeException();
            }

            return info;
        }
    }
}
