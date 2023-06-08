using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Dtos
{
    public class ActivityDtos
    {
        public int Id { get; set; }
        public string ActivityName { get; set; }
        public decimal ActivityPrice { get; set; }
        public int Sorting { get; set; }
        
        public bool IsDeleted { get; set; } = false;
    }
}
