using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using projectStructure.BLL.Services;
using projectStructure.Common.DTOapp.Create;
using projectStructure.DAL;

namespace projectStructure.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeamsController : ControllerBase
    {
        private readonly LinqService _linqService;
        private readonly TeamService _teamService;
        public TeamsController(TeamService teamService, LinqService linqService)
        {
            _teamService = teamService;
            _linqService = linqService;
        }
        [HttpGet]
        public IEnumerable<Team> Get()
        {
            return _teamService.GetAllTeams();
        }
        [HttpGet("{id}")]
        public Team Get([FromRoute] int id)
        {
            return _teamService.GetTeam(id);
        }
        [HttpPost]
        public ActionResult Create([FromBody] TeamCreateDTO proj)
        {
            try
            {
                if (_teamService.Create(proj))
                    return StatusCode(201);
                return BadRequest();
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPut("{id}")]
        public ActionResult Update([FromBody] TeamCreateDTO proj, [FromRoute] int id)
        {
            if (_teamService.Update(proj, id))
                return Ok();

            return BadRequest();
        }
        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            if (_teamService.Delete(id))
                return Ok();

            return BadRequest();
        }
        [HttpGet("sorted")]
        public string GetSortedUserTeams()
        {
            return JsonConvert.SerializeObject(_linqService.GetSortedUsersTeams(), Formatting.Indented);
        }

    }
}
