using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Dtos
{
    public class TicketDtos
    {
        public int Id { get; set; }
        
        public string TicketName { get; set; }
    
        public decimal PriceForAdult { get; set; }
      
        public decimal PriceForKids { get; set; }
        public bool IsDeleted { get; set; } 
        public int Sorting { get; set; }
      
    }
}
