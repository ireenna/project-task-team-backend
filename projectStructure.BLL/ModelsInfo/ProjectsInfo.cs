using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using projectStructure.DAL.Models;

namespace projectStructure.BLL.ModelsInfo
{
    public struct ProjectsInfo
    {
        public Project Project { get; set; }
        public Tasks LongestTaskByDescr { get; set; }
        public Tasks ShortestTaskByName { get; set; }
        public int UsersCount { get; set; }
        public override string ToString()
        {
            return $"Project: {Project.Name}.\n" +
                $"Longest task (by description): {LongestTaskByDescr?.Name ?? "-"}. {LongestTaskByDescr?.Description}\n" +
                $"Shortest task (by name): {ShortestTaskByName?.Name ?? "-"}\n" +
                $"Users count: {UsersCount}\n";
        }
    }
}
