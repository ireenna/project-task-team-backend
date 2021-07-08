using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FakeItEasy;
using projectStructure.BLL.MappingProfiles;
using projectStructure.BLL.Services;
using projectStructure.Common.DTOapp.Create;
using projectStructure.Common.DTOapp.Update;
using projectStructure.DAL;
using Xunit;

namespace projectStructure.BLL.Tests
{
    public class UserServiceTests
    {
        private readonly UserService _userService;
        private IMapper _mapper;
        private IRepository<User> _userRepo;
        private IRepository<Team> _teamRepo;
        public UserServiceTests()
        {
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile<UserProfile>();
                    cfg.AddProfile<ProjectProfile>();
                    cfg.AddProfile<TasksProfile>();
                    cfg.AddProfile<TeamProfile>();
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }
            _userRepo = A.Fake<IRepository<User>>();
            _teamRepo = A.Fake<IRepository<Team>>();
            _userService = new UserService(_mapper, _userRepo, _teamRepo);
        }
        [Fact]
        public void UserCreate_ThenUserInsertHappened()
        {
            var user1 = new UserCreateDTO { BirthDay = DateTime.Now, Email = "somemail@gmail.com", FirstName = "FirstName", LastName = "LastName", TeamId = 1 };
            _userService.Create(user1);
            A.CallTo(() => _userRepo.Insert(A<User>._)).MustHaveHappenedOnceExactly();
        }
        [Fact]
        public void UserCreate_WhenFirst_ThenUserIdIsEqualToZero()
        {
            var user1 = new UserCreateDTO { BirthDay = DateTime.Now, Email = "somemail@gmail.com", FirstName = "FirstName", LastName = "LastName", TeamId = 2 };
            var createdUserId = _userService.Create(user1);
            Assert.Equal(0, createdUserId);
        }

        [Theory]
        [InlineData("2021-01-01", "", "", "", 0)]
        [InlineData("2021-02-01", "email", "fname", "lname", 1)]
        [InlineData("2025-02-01", "somemail@gmail.com", "fname", "lname", 1)]
        public void UserCreate_WhenWrongArguments_ThenThrowArgumentException(DateTime bday, string email, string fname, string lname, int teamid)
        {
            Assert.Throws<ArgumentException>(() => _userService.Create(new UserCreateDTO()
            {
                BirthDay = bday,
                Email = email,
                FirstName = fname,
                LastName = lname,
                TeamId = teamid
            }));
        }

        [Fact]
        public void UserUpdate_ThenRepositoryUserUpdateHappened()
        {
            var updatedUser1 = new UserUpdateDTO { BirthDay = DateTime.Now, Email = "somemail@gmail.com", FirstName = "FirstName", LastName = "LastName", TeamId = 1 };
            _userService.Update(updatedUser1, 1);
            A.CallTo(() => _userRepo.Update(A<User>._)).MustHaveHappenedOnceExactly();
        }

        [Theory]
        [InlineData("2021-01-01", "", "", "", 0)]
        [InlineData("2021-02-01", "email", "fname", "lname", 1)]
        [InlineData("2025-02-01", "somemail@gmail.com", "fname", "lname", 1)]
        public void UserUpdate_WhenWrongArguments_ThenThrowArgumentException(DateTime bday, string email, string fname, string lname, int teamid)
        {
            Assert.Throws<ArgumentException>(() => _userService.Update(new UserUpdateDTO()
            {
                BirthDay = bday,
                Email = email,
                FirstName = fname,
                LastName = lname,
                TeamId = teamid
            }, 1));
        }

        [Fact]
        public void UserUpdate_WhenWrongUserId_ThenThrowArgumentException() //без бд не работает
        {
            var updatedUser1 = new UserUpdateDTO { BirthDay = DateTime.Now, Email = "somemail@gmail.com", FirstName = "FirstName", LastName = "LastName", TeamId = 1 };
            Assert.Throws<ArgumentException>(() => _userService.Update(updatedUser1, -1));
        }
    }
}
