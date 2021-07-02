using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using projectStructure.BLL.Services;
using projectStructure.Common.DTOapp.Create;
using projectStructure.Common.DTOapp.Update;
using projectStructure.DAL;

namespace projectStructure.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly LinqService _linqService;
        private readonly TasksService _tasksService;
        public TasksController(TasksService teamService, LinqService linqService)
        {
            _tasksService = teamService;
            _linqService = linqService;
        }
        [HttpGet]
        public IEnumerable<TasksDAL> Get()
        {
            return _tasksService.GetAllTasks();
        }
        [HttpGet("{id}")]
        public TasksDAL Get([FromRoute] int id)
        {
            return _tasksService.GetTask(id);
        }
        [HttpPost]
        public ActionResult Create([FromBody] TasksCreateDTO proj)
        {
            if (_tasksService.Create(proj))
                return StatusCode(201);

            return BadRequest();
        }
        [HttpPut("{id}")]
        public ActionResult Update([FromBody] TasksUpdateDTO proj, [FromRoute] int id)
        {
            if (_tasksService.Update(proj, id))
                return Ok();

            return BadRequest();
        }
        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            if (_tasksService.Delete(id))
                return Ok();

            return BadRequest();
        }

    }
}
