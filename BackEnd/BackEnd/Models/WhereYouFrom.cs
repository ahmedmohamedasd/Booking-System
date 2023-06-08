using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Models
{
    public class WhereYouFrom
    {
        public int Id { get; set; }
        public string PlaceName { get; set; }
        public bool IsDeleted { get; set; } = false;
        public int Sorting { get; set; }

    }
}
