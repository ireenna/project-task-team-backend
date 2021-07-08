using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using projectStructure.Common.DTOapp.Create;
using projectStructure.Common.DTOapp.Update;
using projectStructure.DAL;
using projectStructure.DAL.Repositories;

namespace projectStructure.BLL.Services
{
    public sealed class UserService : BaseService
    {
        private readonly IRepository<User> _userRepo;
        private readonly IRepository<Team> _teamRepo;
        public UserService(IMapper mapper, IRepository<User> userRepo, IRepository<Team> teamRepo) : base(mapper)
        {
            _userRepo = userRepo;
            _teamRepo = teamRepo;
        }

        public List<User> GetAllUsers()
        {
            return _userRepo.Get().ToList();
        }
        public User GetUser(int id)
        {
            return _userRepo.GetByID(id);
        }
        public int Create(UserCreateDTO item)
        {
            
            var user = _mapper.Map<User>(item);
            _userRepo.Insert(user);
            _userRepo.SaveChanges();
            return user.Id;

        }
        public bool Update(UserUpdateDTO proj, int id)
        {
            if (_userRepo.GetByID(id) is null)
                throw new ArgumentException();

            var oldUser = _userRepo.GetByID(id);
            oldUser.BirthDay = proj.BirthDay;
            oldUser.Email = proj.Email;
            oldUser.FirstName = proj.FirstName;
            oldUser.LastName = proj.LastName;
            if (_teamRepo.GetByID(proj.TeamId) is null)
                throw new ArgumentException();
            oldUser.TeamId = proj.TeamId;
            _userRepo.Update(oldUser);
            _userRepo.SaveChanges();
            return true;

        }
        public bool Delete(int id)
        {
            try
            {
                _userRepo.Delete(id);
                _userRepo.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }
    }
}
