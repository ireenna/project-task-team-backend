using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectStructure.DAL.DAL
{
    public class FullTeamDAL
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<UserDAL> Participants { get; set; }
    }
}
