using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using projectStructure.BLL.ModelsInfo;
using projectStructure.BLL.Services;
using projectStructure.Common.Models;

namespace projectStructure.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InfoController : ControllerBase
    {
        private readonly LinqService _linqService;

        public InfoController(LinqService linqService)
        {
            _linqService = linqService;
        }

        [HttpGet("tasks/{id}")]
        public Dictionary<Project, int> GetQuantityOfUserTasks([FromRoute] int id)
        {
            return _linqService.GetQuantityOfUserTasks(id);
        }
        [HttpGet("users/{id}/tasks")]
        public List<Tasks> GetUserTasks([FromRoute] int id)
        {
            return _linqService.GetUserTasks(id);
        }
        [HttpGet("users/{id}/tasks/finished")]
        public string GetUserFinishedTasks([FromRoute] int id)
        {
            return JsonConvert.SerializeObject(_linqService.GetUserFinishedTasks(id), Formatting.Indented);
        }
        [HttpGet("teams/sorted")]
        public string GetSortedUserTeams()
        {
            return JsonConvert.SerializeObject(_linqService.GetSortedUsersTeams(), Formatting.Indented);
        }
        [HttpGet("users/sorted")]
        public List<IGrouping<User, Tasks>> GetSortedUsersWithTasks()
        {
            return _linqService.GetSortedUsersWithTasks();
        }
        [HttpGet("users/{id}/tasks/info")]
        public UserTaskInfo GetUserTasksInfo([FromRoute]int id)
        {
            return _linqService.GetUserTasksInfo(id);
        }
        [HttpGet("projects/info")]
        public List<ProjectsInfo> GetProjectsInfo()
        {
            return _linqService.GetProjectsInfo();
        }
    }
}
