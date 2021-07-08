using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectStructure.Common.DTOapp.Create
{
    public class TeamCreateDTO
    {
        //private string name;
        [Required]
        [MinLength(1)]
        [MaxLength(20)]
        public string Name { get; set; }
        //{
        //    get
        //    {
        //        return name;
        //    }
        //    set
        //    {
        //        if (value.Length < 1 | value.Length > 20)
        //            throw new ArgumentException();
        //        name = value;
        //    }
        //}
    }
}
