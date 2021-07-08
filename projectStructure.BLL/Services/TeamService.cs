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
        private readonly IRepository<Team> _teamRepo;
        public TeamService(IMapper mapper, IRepository<Team> teamRepo) : base(mapper)
        {
            _teamRepo = teamRepo;
        }

        public IEnumerable<Team> GetAllTeams()
        {
            return _teamRepo.Get();
        }
        public Team GetTeam(int id)
        {
            return _teamRepo.GetByID(id);
        }
        public bool Create(TeamCreateDTO item)
        {
            try
            {
                var team = _mapper.Map<Team>(item);
                _teamRepo.Insert(team);
                _teamRepo.SaveChanges();
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
                var oldTeam = _teamRepo.GetByID(id);
                oldTeam.Name = proj.Name;
                _teamRepo.Update(oldTeam);
                _teamRepo.SaveChanges();
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
                _teamRepo.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }
    }
}
