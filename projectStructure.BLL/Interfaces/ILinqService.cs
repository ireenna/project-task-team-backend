using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using projectStructure.BLL.ModelsInfo;
using projectStructure.DAL;

namespace projectStructure.BLL.Interfaces
{
    public interface ILinqService
    {
        Dictionary<Project, int> GetQuantityOfUserTasks(int id);
        List<Tasks> GetUserTasks(int id);
        List<(int id, string name)> GetUserFinishedTasks(int id);
        List<(int id, string name, List<User> users)> GetSortedUsersTeams();
        List<IGrouping<User, Tasks>> GetSortedUsersWithTasks();
        UserTaskInfo GetUserTasksInfo(int id);
        List<ProjectsInfo> GetProjectsInfo();

    }
}
