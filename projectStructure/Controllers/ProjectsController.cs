using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using projectStructure.BLL.Services;
using projectStructure.Common.Models;

namespace projectStructure.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
    }
}
