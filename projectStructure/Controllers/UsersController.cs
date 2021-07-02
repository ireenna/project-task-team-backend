using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using projectStructure.BLL.Models;
using projectStructure.BLL.ModelsInfo;
using projectStructure.BLL.Services;
using projectStructure.DAL;
using projectStructure.DAL.DAL;

namespace projectStructure.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly LinqService _linqService;

        public UsersController(LinqService linqService)
        {
            _linqService = linqService;
        }
        [HttpGet("{id}/project/alltasks")]
        public Dictionary<FullProjectsDAL, int> GetQuantityOfUserTasks([FromRoute] int id)
        {
            return _linqService.GetQuantityOfUserTasks(id);
        }
        [HttpGet("{id}/tasks/filtered")]
        public List<Tasks> GetUserTasks([FromRoute] int id)
        {
            return _linqService.GetUserTasks(id);
        }
        [HttpGet("{id}/tasks/finished")]
        public string GetUserFinishedTasks([FromRoute] int id)
        {
            return JsonConvert.SerializeObject(_linqService.GetUserFinishedTasks(id), Formatting.Indented);
        }
        [HttpGet("tasks/sorted")]
        public List<IGrouping<UserDAL, FullTasksDAL>> GetSortedUsersWithTasks()
        {
            return _linqService.GetSortedUsersWithTasks();
        }
        [HttpGet("{id}/tasks/info")]
        public UserTaskInfo GetUserTasksInfo([FromRoute] int id)
        {
            return _linqService.GetUserTasksInfo(id);
        }
    }
}
