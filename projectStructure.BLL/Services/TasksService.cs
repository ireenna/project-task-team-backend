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
    public sealed class TasksService : BaseService
    {
        private readonly IRepository<Tasks> _tasksRepo;
        private readonly IRepository<User> _userRepo;
        public TasksService(IMapper mapper, IRepository<Tasks> tasksRepo, IRepository<User> userRepo) : base(mapper)
        {
            _tasksRepo = tasksRepo;
            _userRepo = userRepo;
        }

        public IEnumerable<Tasks> GetAllTasks()
        {
            return _tasksRepo.Get();
        }
        public Tasks GetTask(int id)
        {
            return _tasksRepo.GetByID(id);
        }
        public bool Create(TasksCreateDTO item)
        {
            try
            {
                var task = new Tasks() {
                    CreatedAt = DateTime.Now,
                    FinishedAt = null,
                    Description = item.Description,
                    Name = item.Name,
                    Performer = _userRepo.GetByID(item.PerformerId),
                    //Project = _projRepo.GetByID(item.ProjectId)
                    ProjectId = item.ProjectId
                };
                _tasksRepo.Insert(task);
                _tasksRepo.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }
        public bool Update(TasksUpdateDTO task, int id)
        {
                var oldTask = _tasksRepo.GetByID(id);
                oldTask.Description = task.Description;
                oldTask.FinishedAt = task.FinishedAt;
                oldTask.Name = task.Name;
                oldTask.Performer = _userRepo.GetByID(task.PerformerId);
                oldTask.ProjectId = task.ProjectId;
                oldTask.State = (TaskState)task.State;
                _tasksRepo.Update(oldTask);
                _tasksRepo.SaveChanges();
            return true;

        }
        public bool Delete(int id)
        {
            try
            {
                _tasksRepo.Delete(id);
                _tasksRepo.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }
    }
}
