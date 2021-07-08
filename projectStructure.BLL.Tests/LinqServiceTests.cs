using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FakeItEasy;
using Microsoft.EntityFrameworkCore;
using projectStructure.BLL.MappingProfiles;
using projectStructure.BLL.ModelsInfo;
using projectStructure.BLL.Services;
using projectStructure.Common.DTOapp;
using projectStructure.Common.DTOapp.Create;
using projectStructure.Common.DTOapp.Update;
using projectStructure.DAL;
using projectStructure.DAL.Repositories;
using Xunit;

namespace projectStructure.BLL.Tests
{
    public class LinqServiceTests 
    {
        private readonly LinqService _linqService;
        private readonly UserService _userService;
        private readonly ProjectService _projService;
        private readonly TasksService _tasksService;
        private IMapper _mapper;
        private IRepository<Project> _projRepo;
        private IRepository<User> _userRepo;
        private IRepository<Team> _teamRepo;
        private IRepository<Tasks> _taskRepo;
        private ProjectsDbContext _context;
        public LinqServiceTests()
        {
            var dbContextOptions = new DbContextOptionsBuilder<ProjectsDbContext>().UseInMemoryDatabase(databaseName: "TestDB");
            _context = new ProjectsDbContext(dbContextOptions.Options);
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
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
            //witn in memory db
            _projRepo = new BaseRepository<Project>(_context);
            _teamRepo = new BaseRepository<Team>(_context);
            _taskRepo = new BaseRepository<Tasks>(_context);
            _userRepo = new BaseRepository<User>(_context);

            //without in mempry db
            //_projRepo = A.Fake<IRepository<Project>>();
            //_userRepo = A.Fake<IRepository<User>>();
            //_teamRepo = A.Fake<IRepository<Team>>();
            //_taskRepo = A.Fake<IRepository<Tasks>>();
            _linqService = new LinqService(_mapper, _projRepo, _userRepo, _teamRepo, _taskRepo);
            _userService = new UserService(_mapper, _userRepo, _teamRepo);
            _projService = new ProjectService(_mapper, _projRepo, _teamRepo, _userRepo);
            _tasksService = new TasksService(_mapper, _taskRepo, _userRepo);
        }

        [Fact]
        public void GetQuantityOfUserTasks_WhenUserDoesntHaveAnyProjects_ThenDontThrowError()
        {
            var user1 = new UserCreateDTO { BirthDay = DateTime.Now, Email = "a@gmail.com", FirstName = "FirstName", LastName = "LastName", TeamId = 1 };
            int createdUserId = _userService.Create(user1);
            Assert.NotNull(_linqService.GetQuantityOfUserTasks(createdUserId));
        }

        [Fact]
        public void GetQuantityOfUserTasks_ThenGetDictionary()
        {
            Assert.IsType<Dictionary<Project, int>>(_linqService.GetQuantityOfUserTasks(1));
        }
        [Fact]
        public void GetQuantityOfUserTasks_WhenDataIsFull_ThenGetDictionary()
        {
            var user1 = new UserCreateDTO { BirthDay = DateTime.Now, Email = "a@gmail.com", FirstName = "FirstName", LastName = "LastName", TeamId = 1 };
            _userService.Create(user1);
            var lastUserId = _userRepo.Get().LastOrDefault().Id;
            var project1 = new ProjectCreateDTO { AuthorId = lastUserId, Deadline = new DateTime(2021, 08, 08), Description = "some desc", Name = "projName", TeamId = 1 };
            _projService.Create(project1);
            var lastProjId = _projRepo.Get().LastOrDefault().Id;
            var taskForProject1 = new TasksCreateDTO { PerformerId = 1, Description = "Some description", Name = "Name", ProjectId = lastProjId };
            _tasksService.Create(taskForProject1);
            
            Assert.Single(_linqService.GetQuantityOfUserTasks(lastUserId));
            Assert.Equal(1, _linqService.GetQuantityOfUserTasks(lastUserId).First().Value);
        }
        [Fact]
        public void GetUserTasks_ThenGetTasksList()
        {
            Assert.IsType<List<Tasks>>(_linqService.GetUserTasks(1));
        }
        [Fact]
        public void GetUserTasks_WhenDataIsFull_ThenGetTasksList()
        {
            var user1 = new UserCreateDTO { BirthDay = DateTime.Now, Email = "a@gmail.com", FirstName = "FirstName", LastName = "LastName", TeamId = 1 };
            _userService.Create(user1);
            var lastUserId = _userRepo.Get().LastOrDefault().Id;
            var project1 = new ProjectCreateDTO { AuthorId = lastUserId, Deadline = new DateTime(2021, 08, 08), Description = "some desc", Name = "projName", TeamId = 1 };
            _projService.Create(project1);
            var lastProjId = _projRepo.Get().LastOrDefault().Id;
            var taskForProject1 = new TasksCreateDTO { PerformerId = lastUserId, Description = "Some description", Name = "Name", ProjectId = lastProjId };
            _tasksService.Create(taskForProject1);
            var lastTaskId = _taskRepo.Get().LastOrDefault().Id;

            Assert.Single(_linqService.GetUserTasks(lastUserId));
            Assert.Equal(lastTaskId, _linqService.GetUserTasks(lastUserId).Last().Id);
        }
        [Fact]
        public void GetUserFinishedTasks_ThenGetListIdName()
        {
            Assert.IsType<List<(int id, string name)>>(_linqService.GetUserFinishedTasks(1));
        }
        [Fact]
        public void GetUserFinishedTasks_WhenDataIsFull_ThenGetListIdName()
        {
            var user1 = new UserCreateDTO { BirthDay = DateTime.Now, Email = "a@gmail.com", FirstName = "FirstName", LastName = "LastName", TeamId = 1 };
            _userService.Create(user1);
            var lastUserId = _userRepo.Get().LastOrDefault().Id;
            var taskForUser1 = new TasksCreateDTO { PerformerId = lastUserId, Description = "Some description", Name = "Name", ProjectId = 1 };
            _tasksService.Create(taskForUser1);
            _tasksService.Create(taskForUser1);
            var lastTaskId = _taskRepo.Get().LastOrDefault().Id;
            Assert.Equal(2, _linqService.GetUserTasks(lastUserId).Count);
            Assert.Equal(taskForUser1.Name, _linqService.GetUserTasks(lastUserId)[0].Name);
            Assert.Equal(lastTaskId, _linqService.GetUserTasks(lastUserId)[1].Id);
        }
        [Fact]
        public void GetUserTasksInfo_ThenGetList()
        {
            Assert.IsType<UserTaskInfo>(_linqService.GetUserTasksInfo(1));
        }

        [Fact]
        public void GetSortedUsersWithTasks_ThenListIGrouping()
        {
            Assert.IsType<List<IGrouping<User, Tasks>>>(_linqService.GetSortedUsersWithTasks());
        }

        [Fact]
        public void GetProjectsInfo_ThenListProjectsInfo()
        {
            Assert.IsType<List<ProjectsInfo>>(_linqService.GetProjectsInfo());
        }

        [Fact]
        public void GetQuantityOfUserTasks_WhenUserDoesntExist_ThenThrowError()
        {
            var users = _userService.GetAllUsers();
            int nonExistableUserById = 0;
            if (users.Count > 0)
                nonExistableUserById = users[users.Count - 1].Id + 1;

            Assert.Throws<ArgumentOutOfRangeException>(() => _linqService.GetQuantityOfUserTasks(nonExistableUserById));
        }
        

        [Fact]
        public void GetUserTasks_WhenUserDoesntExist_ThenThrowError()
        {
            var users = _userService.GetAllUsers();
            int nonExistableUserById = 0;
            if (users.Count > 0)
                nonExistableUserById = users[users.Count - 1].Id + 1;

            Assert.Throws<ArgumentOutOfRangeException>(() => _linqService.GetUserTasks(nonExistableUserById));
        }
        
        [Fact]
        public void GetUserFinishedTasks_WhenUserDoesntExist_ThenThrowError()
        {
            var users = _userService.GetAllUsers();
            int nonExistableUserById = 0;
            if (users.Count > 0)
                nonExistableUserById = users[users.Count - 1].Id + 1;

            Assert.Throws<ArgumentOutOfRangeException>(() => _linqService.GetUserFinishedTasks(nonExistableUserById));
        }
        
        [Fact]
        public void GetUserTasksInfo_WhenUserDoesntExist_ThenThrowError()
        {
            var users = _userService.GetAllUsers();
            int nonExistableUserById = 0;
            if (users.Count > 0)
                nonExistableUserById = users[users.Count - 1].Id + 1;

            Assert.Throws<ArgumentOutOfRangeException>(() => _linqService.GetUserTasksInfo(nonExistableUserById));
        }
        [Fact]
        public void GetUserUnfinishedTasks_WhenUserDoesntExist_ThenThrowError()
        {
            var users = _userService.GetAllUsers();
            int nonExistableUserById = 0;
            if (users.Count > 0)
                nonExistableUserById = users[users.Count - 1].Id + 1;

            Assert.Throws<ArgumentOutOfRangeException>(() => _linqService.GetUserTasksInfo(nonExistableUserById));
        }
        [Fact]
        public void GetUserUnfinishedTasks_WhenDataIsFull_ThenGetListIdName()
        {
            var user1 = new UserCreateDTO { BirthDay = DateTime.Now, Email = "a@gmail.com", FirstName = "FirstName", LastName = "LastName", TeamId = 1 };
            _userService.Create(user1);
            var lastUserId = _userRepo.Get().LastOrDefault().Id;
            var taskForUser1 = new TasksCreateDTO { PerformerId = lastUserId, Description = "Some description", Name = "Name", ProjectId = 1 };
            _tasksService.Create(taskForUser1);
            _tasksService.Create(taskForUser1);
            var lastTaskId = _taskRepo.Get().LastOrDefault().Id;
            var updatedLastTaskToDone = new TasksUpdateDTO
            {
                PerformerId = lastUserId,
                ProjectId = 1,
                Name= "name",
                Description = "desc",
                FinishedAt = DateTime.Now,
                State = Common.TaskStateDTO.Done
            };
            _tasksService.Update(updatedLastTaskToDone, lastTaskId);
            Assert.Equal(2, _linqService.GetUserTasks(lastUserId).Count);
        }
    }
}
