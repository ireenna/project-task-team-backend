using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using projectStructure.BLL.Models;

namespace projectStructure.BLL.ModelsInfo
{
    public class UserTaskInfo
    {
        public User User { get; set; }
        public Project LastProject { get; set; }
        public int LastProjectTasksCount { get; set; }
        public int RejectedTasks { get; set; }
        public Tasks TheLongestTask { get; set; }
    }
}