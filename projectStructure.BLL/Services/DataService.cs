using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using Newtonsoft.Json;
using projectStructure.Common;
using projectStructure.DAL.Models;
using projectStructure.Common.DTO;

namespace projectStructure.BLL.Services
{
    public sealed class DataService : BaseService
    {
        public DataService(IMapper mapper) : base(mapper) { }

        private readonly HttpClient client = new HttpClient() { BaseAddress = new Uri("https://bsa21.azurewebsites.net/api/") };
        public async Task<Boolean> GetAll()
        {
            var projects = await GetProjects();
            var tasks = await GetTasks();
            var users = await GetUsers();
            var teams = await GetTeams();

            _context.tasks = tasks.Join(users, t => t.PerformerId, u => u.Id, (t, u) => new Tasks(t, _mapper.Map<User>(u))).ToList();

            _context.teams = teams.GroupJoin(users, t => t.Id, u => u.TeamId, (t, u) => new Team(t, u.Select(u=>_mapper.Map<User>(u)))).ToList();

            _context.users = users.Select(u => _mapper.Map<User>(u)).ToList();

            var all = projects.GroupJoin(_context.tasks, p => p.Id, tks => tks.ProjectId, (p, tks) => new { project = p, tasks = tks })
                .Join(users, p => p.project.AuthorId, u => u.Id, (p, u) => new { project = p.project, tasks = p.tasks, author = u })
                .Join(_context.teams, p => p.project.TeamId, tms => tms.Id, (p, tms) => new Project(p.project, p.tasks, _mapper.Map<User>(p.author), tms)).ToList();

            _context.projects = all;

            return Convert.ToBoolean(all.Count);
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
