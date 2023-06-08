using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Models
{
    public class HowYouKnowUs
    {
        public int Id { get; set; }
        public string WayName { get; set; }
        public int Sorting { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
