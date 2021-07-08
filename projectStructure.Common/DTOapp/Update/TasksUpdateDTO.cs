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
        private string name;
        public string Description { get; set; }
        public TaskStateDTO State { get; set; }
        private DateTime? finishedAt;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (value.Length < 1)
                    throw new ArgumentException();
                name = value;
            }
        }
        public DateTime FinishedAt
        {
            get
            {
                return finishedAt.Value;
            }
            set
            {
                if (value.Date > DateTime.Now)
                    throw new ArgumentException();
                finishedAt = value;
            }
        }
    }
}
