using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.ViewModel
{
    public class GuestStatistics
    {
        public string Name { get; set; }
        public List<SeriesStatistics> Series { get; set; }
    }
}
