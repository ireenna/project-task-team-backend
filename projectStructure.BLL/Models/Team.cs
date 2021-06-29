using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using projectStructure.Common.DTO;

namespace projectStructure.BLL.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<User> Participants { get; set; }
        public Team(TeamDTO t, IEnumerable<User> u)
        {
            Id = t.Id;
            Name = t.Name;
            CreatedAt = t.CreatedAt;
            Participants = u.ToList();
        }
    }
}
