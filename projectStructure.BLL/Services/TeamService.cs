using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using projectStructure.Common.DTOapp.Create;
using projectStructure.DAL;
using projectStructure.DAL.Repositories;

namespace projectStructure.BLL.Services
{
    public sealed class TeamService : BaseService
    {
        private readonly TeamRepository<TeamDAL> _teamRepo;
        public TeamService(IMapper mapper) : base(mapper)
        {
            _teamRepo = new TeamRepository<TeamDAL>();
        }

        public IEnumerable<TeamDAL> GetAllTeams()
        {
            return _teamRepo.Get();
        }
        public TeamDAL GetTeam(int id)
        {
            return _teamRepo.Get(id);
        }
        public bool Create(TeamCreateDTO item)
        {
            try
            {
                var team = _mapper.Map<TeamDAL>(item);
                _teamRepo.Create(team);
                return true;
            }
            catch
            {
                return false;
            }

        }
        public bool Update(TeamCreateDTO proj, int id)
        {
            try
            {
                var oldTeam = _teamRepo.Get(id);
                oldTeam.Name = proj.Name;
                _teamRepo.Update(oldTeam);
                return true;
            }
            catch
            {
                return false;
            }

        }
        public bool Delete(int id)
        {
            try
            {
                _teamRepo.Delete(id);
                return true;
            }
            catch
            {
                return false;
            }

        }
    }
}
