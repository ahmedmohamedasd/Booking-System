using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string EventName { get; set; }
        public DateTime DateOfEvent { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal EventPrice { get; set; }
        public bool IsDeleted { get; set; } = false;
        public ICollection<GuestEvent> GuestEvents { get; set; }
    }
}
