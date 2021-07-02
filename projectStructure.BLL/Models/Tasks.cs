﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using projectStructure.Common.DTO;

namespace projectStructure.BLL.Models
{
    public class Tasks
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public int PerformerId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public TaskStateDTO State { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? FinishedAt { get; set; }
    }
}