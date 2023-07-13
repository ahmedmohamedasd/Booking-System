
using BackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Dtos
{
    public class EventDtos
    {
        public int Id { get; set; }
        public string EventName { get; set; }
        public DateTime DateOfEvent { get; set; }
       
        public decimal EventPrice { get; set; }
        public bool IsDeleted { get; set; } = false;
        public List<GuestEvent> GuestEvents { get; set; }
        public int Count { get; set; }
        public string Status { get; set; }
    }
}
