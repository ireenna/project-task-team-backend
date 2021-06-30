using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using projectStructure.Common.Models;
using projectStructureApp.ModelsDTOapp;

namespace projectStructureApp.ModelsDTOapp
{
    public class UserTaskInfoDTOapp
    {
        public UserDTOapp User { get; set; }
        public ProjectDTOapp LastProject { get; set; }
        public int LastProjectTasksCount { get; set; }
        public int RejectedTasks { get; set; }
        public TasksDTOapp TheLongestTask { get; set; }

        public override string ToString()
        {
            if (User == null)
            {
                return "This user doesn't have any tasks.";
            }
            return $"User: {User.FirstName} {User.LastName}.\n" +
                $"Last project: {LastProject?.Name ?? "no last projects"}\n" +
                $"Last project all tasks count: {LastProjectTasksCount}\n" +
                $"Unfinished or canceled tasks count for {User.FirstName}: {RejectedTasks}\n" +
                $"The longest task for {User.FirstName}: {TheLongestTask?.Name ?? "no tasks"}";
        }
    }
}