using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.ViewModel
{
    public class ActivityModel
    {
        public int Id { get; set; }
        public string ActivityName { get; set; }
        public decimal ActivityPrice { get; set; }
        public int Sorting { get; set; }
        public bool IsIncluded { get; set; }
        public int Quantity { get; set; }
        public decimal SubTotal { get; set; }
    }
}
