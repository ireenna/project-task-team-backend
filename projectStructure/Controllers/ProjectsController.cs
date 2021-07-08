using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using projectStructure.BLL.ModelsInfo;
using projectStructure.BLL.Services;
using projectStructure.Common.DTOapp;
using projectStructure.DAL;

namespace projectStructure.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly ProjectService _projectService;
        private readonly LinqService _linqService;

        public ProjectsController(ProjectService projectService, LinqService linqService)
        {
            _projectService = projectService;
            _linqService = linqService;
        }

        [HttpGet]
        public IEnumerable<Project> Get()
        {
            return _projectService.GetAllProjects();
        }
        [HttpGet("{id}")]
        public Project Get([FromRoute] int id)
        {
            return _projectService.GetProject(id);
        }
        [HttpPost]
        public ActionResult Create([FromBody] ProjectCreateDTO proj)
        {
            try
            {
                if (_projectService.Create(proj)) return StatusCode(201);
                return BadRequest();
            }
            catch
            {
                return BadRequest();
            }
            
           
        }
        [HttpPut("{id}")]
        public ActionResult Update([FromBody] ProjectUpdateDTO proj, [FromRoute] int id)
        {
            if (_projectService.Update(proj, id))
                return Ok();

            return BadRequest();
        }
        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            if (_projectService.Delete(id))
                return Ok();

            return BadRequest();
        }
        [HttpGet("info")]
        public List<ProjectsInfo> GetProjectsInfo()
        {
            return _linqService.GetProjectsInfo();
        }
    }
}
