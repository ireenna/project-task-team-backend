using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using projectStructure.BLL.Services;
using projectStructure.Common.Models;
using projectStructure.Common.DTOapp;

namespace projectStructure.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly DataService _dataService;
        private readonly ProjectService _projectService;

        public ProjectsController(DataService dataService, ProjectService projectService)
        {
            _dataService = dataService;
            _projectService = projectService;
        }

        [HttpGet]
        public IEnumerable<Project> Get()
        {
            return _projectService.GetAllProjects();
        }
        [HttpPost]
        public ActionResult Create([FromBody] ProjectCreateDTO proj)
        {
            return _projectService.Create(proj);
        }
    }
}
