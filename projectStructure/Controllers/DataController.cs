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
    public class DataController : ControllerBase
    {
        private readonly DataService _dataService;

        public DataController(DataService dataService)
        {
            _dataService = dataService;
        }

        [HttpGet]
        public async Task<ActionResult> RefreshInfo()
        {
            return Ok(await _dataService.GetAll());
        }
    }
}
