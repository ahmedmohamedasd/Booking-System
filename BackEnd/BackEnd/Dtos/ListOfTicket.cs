using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Dtos
{
    public class ListOfTicket
    {
        public int Id { get; set; }
        public string ticketName { get; set; }
        public decimal PriceForAdult { get; set; }
        public decimal PriceForKids { get; set; }
        public int QuantityForAdult { get; set; }
        public int QuantityForKids { get; set; }
        public decimal SubTotalAdult { get; set; }
        public decimal SubTotalKids { get; set; }


    }
}
