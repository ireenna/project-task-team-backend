using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectStructure.Common.DTOapp.Update
{
    public class TasksUpdateDTO
    {
        public int ProjectId { get; set; }
        public int PerformerId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public TaskStateDTO State { get; set; }
        public DateTime? FinishedAt { get; set; }
    }
}
