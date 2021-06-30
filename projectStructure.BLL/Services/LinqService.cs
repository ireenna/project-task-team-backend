using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using projectStructure.BLL.ModelsInfo;
using projectStructure.Common.Models;
using projectStructure.DAL;

namespace projectStructure.BLL.Services
{
    public sealed class LinqService : BaseService
    {
        private readonly ProjectRepository<Project> _projRepo;
        public LinqService(IMapper mapper) : base(mapper)
        {
            _projRepo = new ProjectRepository<Project>();
        }
        public Dictionary<Project, int> GetQuantityOfUserTasks(int id)
        {
            var info = _projRepo.Get()
                .Where(x => x.Author.Id == id)
                .ToDictionary(x => x, x => x.Tasks.Count());

            return info;
        }
        public List<Tasks> GetUserTasks(int id)
        {
            var info = _projRepo.Get()
                .SelectMany(pr => pr.Tasks)
                .Where(x => x.Performer.Id == id && x.Name.Length < 45)
                .ToList();

            return info;
        }
        public List<(int id, string name)> GetUserFinishedTasks(int id)
        {
            var info = _projRepo.Get()
                .SelectMany(p => p.Tasks)
                .Where(x => x.Performer.Id == id && x.FinishedAt <= DateTime.Now && x.FinishedAt >= new DateTime(2021, 1, 1))
                .Select(x => (id: x.Id, name: x.Name))
                .ToList();

            return info;
        }
        public List<(int id, string name, List<User> users)> GetSortedUsersTeams()
        {
            var info = _projRepo.Get()
                .Select(x => x.Team).Distinct()
                .Where(x => x.Participants.All(x => x.BirthDay <= DateTime.Now.AddYears(-10)))
                .Select(x => (x.Id, x.Name, x.Participants.OrderByDescending(p => p.RegisteredAt).ToList())).ToList();

            return info;
        }
        public List<IGrouping<User, Tasks>> GetSortedUsersWithTasks()
        {
            var info = _projRepo.Get()
                .SelectMany(p => p.Tasks)
                .OrderByDescending(t => t.Name.Length)
                .GroupBy(t => t.Performer)
                .OrderBy(x => x.Key.FirstName)
                .ToList();

            return info;
        }
        public UserTaskInfo GetUserTasksInfo(int id)
        {
            var info = _projRepo.Get()
                .SelectMany(p => p.Tasks)
                .Where(x => x.Performer.Id == id)
                .GroupBy(x => x.Performer)
                .Where(x => x.Key.Id == id)
                .Select(x => new {
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

            return info;
        }
        public List<ProjectsInfo> GetProjectsInfo()
        {
            var info = _projRepo.Get().Select(p => new ProjectsInfo
            {
                Project = p,
                LongestTaskByDescr = p.Tasks.OrderByDescending(t => t.Description.Length).FirstOrDefault(),
                ShortestTaskByName = p.Tasks.OrderBy(t => t.Name.Length).FirstOrDefault(),
                UsersCount = p.Description.Length > 20 ^ p.Tasks.Count < 3 ? p.Team.Participants.Count : 0
            }).ToList();

            return info;
        }
    }
}
