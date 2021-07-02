using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using projectStructure.BLL.Models;
using projectStructure.BLL.ModelsInfo;
using projectStructure.DAL;
using projectStructure.DAL.DAL;
using projectStructure.DAL.Repositories;

namespace projectStructure.BLL.Services
{
    public sealed class LinqService : BaseService
    {
        private readonly FullProjectRepository<FullProjectsDAL> _projRepo;
        public LinqService(IMapper mapper) : base(mapper)
        {
            _projRepo = new FullProjectRepository<FullProjectsDAL>();
        }
        public Dictionary<FullProjectsDAL, int> GetQuantityOfUserTasks(int id)
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
                .Select(x=>_mapper.Map<Tasks>(x))
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
                .Select(x => (x.Id, x.Name, x.Participants.OrderByDescending(p => p.RegisteredAt).Select(x=>_mapper.Map<User>(x)).ToList())).ToList();

            return info;
        }
        public List<IGrouping<UserDAL, FullTasksDAL>> GetSortedUsersWithTasks()
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
                    User = _mapper.Map<User>(x.user),
                    LastProject = _mapper.Map<Project>(x.lastProject),
                    LastProjectTasksCount = x.lastProject?.Tasks.Count() ?? 0,
                    RejectedTasks = x.tasks.Where(x => x.FinishedAt == null | (int)x.State == 2).Count(),
                    TheLongestTask = x.tasks.OrderByDescending(x => (x.FinishedAt ?? DateTime.Now) - x.CreatedAt).Select(x=>_mapper.Map<Tasks>(x)).FirstOrDefault()
                }).FirstOrDefault();

            return info;
        }
        public List<ProjectsInfo> GetProjectsInfo()
        {
            var info = _projRepo.Get().Select(p => new ProjectsInfo()
            {
                Project = _mapper.Map<Project>(p),
                LongestTaskByDescr = p.Tasks.OrderByDescending(t => t.Description.Length).Select(x=>_mapper.Map<Tasks>(x)).FirstOrDefault(),
                ShortestTaskByName = p.Tasks.OrderBy(t => t.Name.Length).Select(x=>_mapper.Map<Tasks>(x)).FirstOrDefault(),
                UsersCount = p.Description.Length > 20 ^ p.Tasks.Count < 3 ? p.Team.Participants.Count : 0
            }).ToList();

            return info;
        }
    }
}
