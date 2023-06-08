using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        [Required]
        public string TicketName { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal PriceForAdult { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal PriceForKids { get; set; }
        public bool IsDeleted { get; set; } = false;
        public int Sorting { get; set; }
        public ICollection<GuestTicket> GuestTickets { get; set; }

    }
}
