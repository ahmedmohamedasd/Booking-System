using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.ViewModel
{
    public class AreaViewModel
    {
        public string AreaName { get; set; }
        public int Ranking { get; set; }
        public bool hasBooked { get; set; } = false;
        public bool Closed { get; set; } = false;
        public string GuestName { get; set; } = null;
        public string Note { get; set; } = null;
        public bool IsSelected { get; set; } = false;
        public int AreaId { get; set; }
        public DateTime DateOfBooking { get; set; }
    }
}
