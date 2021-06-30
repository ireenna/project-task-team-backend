using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectStructureApp.ModelsDTOapp
{
    public struct ProjectsInfoDTOapp
    {
        public ProjectDTOapp Project { get; set; }
        public TasksDTOapp LongestTaskByDescr { get; set; }
        public TasksDTOapp ShortestTaskByName { get; set; }
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
