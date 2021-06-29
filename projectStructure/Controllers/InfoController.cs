using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using projectStructure.BLL.Services;
using projectStructure.DAL.Models;

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

        [HttpGet("tasks")]
        public Dictionary<Project, int> GetQuantityOfUserTasks(int id)
        {
            return _linqService.GetQuantityOfUserTasks(id);
        }
    }
}
