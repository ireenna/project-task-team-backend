using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FakeItEasy;
using projectStructure.BLL.MappingProfiles;
using projectStructure.BLL.Services;
using projectStructure.Common;
using projectStructure.Common.DTOapp.Update;
using projectStructure.DAL;
using Xunit;

namespace projectStructure.BLL.Tests
{
    public class TasksServiceTests
    {
        private readonly TasksService _tasksService;
        private IMapper _mapper;
        private IRepository<User> _userRepo;
        private IRepository<Tasks> _taskRepo;
        public TasksServiceTests()
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
            _taskRepo = A.Fake<IRepository<Tasks>>();
            _tasksService = new TasksService(_mapper, _taskRepo, _userRepo);
        }
        [Fact]
        public void TaskUpdate_ThenTaskRepoUpdateHappened()
        {
            var updatedTask = new TasksUpdateDTO { State = Common.TaskStateDTO.Done, FinishedAt = new DateTime(2021,06,06), Description = "", Name = "somename", PerformerId = 1, ProjectId = 1 };
            var isUpdatedTask = _tasksService.Update(updatedTask, 1);
            A.CallTo(() => _taskRepo.Update(A<Tasks>._)).MustHaveHappenedOnceExactly();
        }
        [Theory]
        [InlineData("2021-01-01", "", "", 1, 1, TaskStateDTO.InProgress)]
        [InlineData("2025-01-01", "some", "text", 1, 1, TaskStateDTO.ToDo)]
        public void TaskUpdate_WhenWrongArguments_ThenThrowArgumentException(DateTime finish, string desc, string name, int perfId, int projId, TaskStateDTO state)
        {
            Assert.Throws<ArgumentException>(() => _tasksService.Update(new TasksUpdateDTO()
            {
                FinishedAt = finish,
                Description =desc,
                Name = name,
                PerformerId = perfId,
                ProjectId = projId,
                State = state
            }, 1));
        }
    }
}
