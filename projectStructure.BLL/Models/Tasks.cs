using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using projectStructure.Common.DTO;

namespace projectStructure.DAL.Models
{
    public class Tasks
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public User Performer { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public TaskState State { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? FinishedAt { get; set; }

        public Tasks(TasksDTO task, User u)
        {
            Id = task.Id;
            ProjectId = task.ProjectId;
            Performer = u;
            Name = task.Name;
            Description = task.Description;
            State = (TaskState)task.State;
            CreatedAt = task.CreatedAt;
            FinishedAt = task.FinishedAt;
        }
    }
}
