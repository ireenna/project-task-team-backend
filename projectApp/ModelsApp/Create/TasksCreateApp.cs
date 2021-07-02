using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectStructureApp.ModelsApp.Create
{
    public class TasksCreateApp
    {
        public int ProjectId { get; set; }
        public int PerformerId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
