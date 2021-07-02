using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using projectStructureApp.ModelsDTOapp;

namespace projectStructureApp.ModelsApp.Update
{
    public class TasksUpdateApp
    {
        public int ProjectId { get; set; }
        public int PerformerId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public TaskStateDTOapp State { get; set; }
        public DateTime? FinishedAt { get; set; }
    }
}
