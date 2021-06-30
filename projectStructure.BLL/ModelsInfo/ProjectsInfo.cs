using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using projectStructure.Common.Models;

namespace projectStructure.BLL.ModelsInfo
{
    public struct ProjectsInfo
    {
        public Project Project { get; set; }
        public Tasks LongestTaskByDescr { get; set; }
        public Tasks ShortestTaskByName { get; set; }
        public int UsersCount { get; set; }
    }
}
