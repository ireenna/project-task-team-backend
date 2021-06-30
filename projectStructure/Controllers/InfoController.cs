using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using projectStructure.BLL.Services;
using projectStructure.Common.Models;

namespace projectStructure.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
    }
}
