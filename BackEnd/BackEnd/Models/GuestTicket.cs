using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Models
{
    public class GuestTicket
    {
        [ForeignKey("Guest")]
        public int GuestId { get; set; }
        public Guest Guest { get; set; }

        [ForeignKey("Ticket")]
        public int TicketId { get; set; }
        public Ticket Ticket { get; set; }

        public int CountOfAdult { get; set; } = 0;
        public int CountOfKids { get; set; } = 0;

        [Column(TypeName ="decimal(18,2)")]
        public decimal? PriceForAdult { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal? PriceForKids { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateOfBooking { get; set; }
    }
}
