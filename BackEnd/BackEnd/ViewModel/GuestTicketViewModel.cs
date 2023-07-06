using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.ViewModel
{
    public class GuestTicketViewModel
    {
        public string TicketName { get; set; }
        public int CountOfAdult { get; set; }
        public int CountOfKids { get; set; }
        public decimal PriceForAdult { get; set; }
        public decimal PriceForKids { get; set; }
        public decimal SubTotal { get; set; }
    }
}
