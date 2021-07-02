using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using Newtonsoft.Json;
using projectStructure.BLL.Models;
using projectStructure.Common;
using projectStructure.Common.DTO;
using projectStructure.DAL;
using projectStructure.DAL.DAL;

namespace projectStructure.BLL.Services
{
    public sealed class DataService : BaseService
    {
        public DataService(IMapper mapper) : base(mapper) { }

        private readonly HttpClient client = new HttpClient() { BaseAddress = new Uri("https://bsa21.azurewebsites.net/api/") };
        public async Task<Boolean> GetAll()
        {
            var projects = await GetProjects();
            _context.projects = projects.Select(x => _mapper.Map<ProjectDAL>(x)).ToList();
            var tasks = await GetTasks();
            _context.tasks = tasks.Select(x => _mapper.Map<TasksDAL>(x)).ToList();
            var users = await GetUsers();
            _context.users = users.Select(x => _mapper.Map<UserDAL>(x)).ToList();
            var teams = await GetTeams();
            _context.teams = teams.Select(x => _mapper.Map<TeamDAL>(x)).ToList();

            var fullteams = teams.Select(x => new FullTeamDAL()
            {
                CreatedAt = x.CreatedAt,
                Id = x.Id,
                Name = x.Name,
                Participants =users.Where(y=>y.TeamId == x.Id).Select(x=>_mapper.Map<UserDAL>(x)).ToList()
            }).ToList();

            var fulltasks = tasks.Select(x => new FullTasksDAL()
            {
                CreatedAt = x.CreatedAt,
                Id = x.Id,
                Name = x.Name,
                FinishedAt = x.FinishedAt,
                Description = x.Description,
                Performer = users.Where(y => y.Id == x.PerformerId).Select(x => _mapper.Map<UserDAL>(x)).FirstOrDefault()
            }).ToList();

            _context.fullprojects = projects.Select(x => new FullProjectsDAL()
            {
                Id = x.Id,
                Author = _mapper.Map<UserDAL>(users.Where(y => y.Id == x.AuthorId).FirstOrDefault()),
                CreatedAt = x.CreatedAt,
                Deadline = x.Deadline,
                Description = x.Description,
                Name = x.Name,
                Tasks = fulltasks.Where(y => y.ProjectId == x.Id).ToList(),
                Team = fullteams.Where(y=>y.Id == x.TeamId).FirstOrDefault()
            }).ToList();


            return Convert.ToBoolean(_context.projects.Count);
        }
        public async Task<List<ProjectDTO>> GetProjects()
        {
            HttpResponseMessage response = await client.GetAsync("Projects");
            string strResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<ProjectDTO>>(strResponse);
        }
        public async Task<List<TasksDTO>> GetTasks()
        {
            HttpResponseMessage response = await client.GetAsync("Tasks");
            string strResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<TasksDTO>>(strResponse);
        }
        public async Task<List<UserDTO>> GetUsers()
        {
            HttpResponseMessage response = await client.GetAsync("Users");
            string strResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<UserDTO>>(strResponse);
        }
        public async Task<List<TeamDTO>> GetTeams()
        {
            HttpResponseMessage response = await client.GetAsync("Teams");
            string strResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<TeamDTO>>(strResponse);
        }
    }
}
