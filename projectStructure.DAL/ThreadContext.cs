using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using projectStructure.DAL.DAL;

namespace projectStructure.DAL
{
    public class ThreadContext
    {
        public List<ProjectDAL> projects { get; set;}
        public List<TasksDAL> tasks { get; set; }
        public List<UserDAL> users { get; set; }
        public List<TeamDAL> teams { get; set; }
        public List<FullProjectsDAL> fullprojects { get; set; }
        //public List<Project> projInfo { get; set; }

        private static ThreadContext instance;
        private ThreadContext()
        {
            projects = new List<ProjectDAL>();
            tasks = new List<TasksDAL>();
            users = new List<UserDAL>();
            teams = new List<TeamDAL>();
            fullprojects = new List<FullProjectsDAL>();
            //projInfo = new List<Project>();
        }
        public static ThreadContext getInstance()
        {
            if (instance == null)
                instance = new ThreadContext();
            return instance;
        }
        
    }
}
