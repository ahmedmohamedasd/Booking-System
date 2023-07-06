using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.ViewModel
{
    public class GuestActivityViewModel
    {
        public string ActivityName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public bool IsIncluded { get; set; }
    }
}
