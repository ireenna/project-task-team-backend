using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using projectStructure.BLL.ModelsInfo;
using projectStructure.BLL.Services;
using projectStructure.Common.DTOapp.Create;
using projectStructure.Common.DTOapp.Update;
using projectStructure.DAL;

namespace projectStructure.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly LinqService _linqService;
        private readonly UserService _userService;
        public UsersController(UserService userService, LinqService linqService)
        {
            _userService = userService;
            _linqService = linqService;
        }

        [HttpGet]
        public IEnumerable<User> Get()
        {
            return _userService.GetAllUsers();
        }
        [HttpGet("{id}")]
        public User Get([FromRoute] int id)
        {
            return _userService.GetUser(id);
        }
        [HttpPost]
        public ActionResult Create([FromBody] UserCreateDTO proj)
        {
            if (_userService.Create(proj))
                return StatusCode(201);

            return BadRequest();
        }
        [HttpPut("{id}")]
        public ActionResult Update([FromBody] UserUpdateDTO proj, [FromRoute] int id)
        {
            if (_userService.Update(proj, id))
                return Ok();

            return BadRequest();
        }
        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            if (_userService.Delete(id))
                return Ok();

            return BadRequest();
        }
        
        [HttpGet("{id}/project/alltasks")]
        public Dictionary<string, int> GetQuantityOfUserTasks([FromRoute] int id)
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
        public List<IGrouping<User, Tasks>> GetSortedUsersWithTasks()
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
