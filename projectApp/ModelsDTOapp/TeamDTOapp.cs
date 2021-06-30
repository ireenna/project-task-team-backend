﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using projectStructure.Common.DTO;

namespace projectStructureApp.ModelsDTOapp
{
    public class TeamDTOapp
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<UserDTOapp> Participants { get; set; }
    }
}
