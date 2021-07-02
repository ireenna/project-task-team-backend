using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using projectStructure.Common.DTOapp.Create;
using projectStructure.Common.DTOapp.Update;
using projectStructure.DAL;

namespace projectStructure.BLL.Services
{
    public sealed class TasksService : BaseService
    {
        private readonly TasksRepository<TasksDAL> _tasksRepo;
        public TasksService(IMapper mapper) : base(mapper)
        {
            _tasksRepo = new TasksRepository<TasksDAL>();
        }

        public IEnumerable<TasksDAL> GetAllTasks()
        {
            return _tasksRepo.Get();
        }
        public TasksDAL GetTask(int id)
        {
            return _tasksRepo.Get(id);
        }
        public bool Create(TasksCreateDTO item)
        {
            try
            {
                var task = _mapper.Map<TasksDAL>(item);
                _tasksRepo.Create(task);
                return true;
            }
            catch
            {
                return false;
            }

        }
        public bool Update(TasksUpdateDTO task, int id)
        {
            try
            {
                var oldTask = _tasksRepo.Get(id);
                oldTask.Description = task.Description;
                oldTask.FinishedAt = task.FinishedAt;
                oldTask.Name = task.Name;
                oldTask.PerformerId = task.PerformerId;
                oldTask.ProjectId = task.ProjectId;
                oldTask.State = (TaskStateDAL)task.State;
                _tasksRepo.Update(oldTask);
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
                _tasksRepo.Delete(id);
                return true;
            }
            catch
            {
                return false;
            }

        }
    }
}
