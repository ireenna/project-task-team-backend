using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using projectStructure.DAL.Models;

namespace projectStructure.DAL
{
    public class ThreadContext
    {
        public List<Project> projects { get; set;}
        public List<Tasks> tasks { get; set; }
        public List<User> users { get; set; }
        public List<Team> teams { get; set; }
        //public List<Project> projInfo { get; set; }

        private static ThreadContext instance;
        private ThreadContext()
        {
            projects = new List<Project>();
            tasks = new List<Tasks>();
            users = new List<User>();
            teams = new List<Team>();
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
