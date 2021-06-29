using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using projectStructure.DAL.Models;

namespace projectStructure.BLL.ModelsInfo
{
    public struct UserTaskInfo
    {
        public User User { get; set; }
        public Project LastProject { get; set; }
        public int LastProjectTasksCount { get; set; }
        public int RejectedTasks { get; set; }
        public Tasks TheLongestTask { get; set; }

        public override string ToString()
        {
            if (User == null)
            {
                return "This user doesn't have any tasks.";
            }
            return $"User: {User.FirstName} {User.LastName}.\n" +
                $"Last project: {LastProject.Name}\n" +
                $"Last project all tasks count: {LastProjectTasksCount}\n" +
                $"Unfinished or canceled tasks count for {User.FirstName}: {RejectedTasks}\n" +
                $"The longest task for {User.FirstName}: {TheLongestTask.Name}";
        }
    }
}