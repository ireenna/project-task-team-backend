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
        private readonly BaseRepository<User> _userRepo;
        public UserService(IMapper mapper, ProjectsDbContext context) : base(mapper, context)
        {
            _userRepo = new BaseRepository<User>(context);
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _userRepo.Get();
        }
        public User GetUser(int id)
        {
            return _userRepo.GetByID(id);
        }
        public bool Create(UserCreateDTO item)
        {
            try
            {
                var user = _mapper.Map<User>(item);
                _userRepo.Insert(user);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }
        public bool Update(UserUpdateDTO proj, int id)
        {
            try
            {
                var oldUser = _userRepo.GetByID(id);
                oldUser.BirthDay = proj.BirthDay;
                oldUser.Email = proj.Email;
                oldUser.FirstName = proj.FirstName;
                oldUser.LastName = proj.LastName;
                oldUser.TeamId = proj.TeamId;
                _userRepo.Update(oldUser);
                _context.SaveChanges();
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
                _userRepo.Delete(id);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }
    }
}
