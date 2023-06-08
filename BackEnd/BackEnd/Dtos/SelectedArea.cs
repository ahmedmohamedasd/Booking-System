using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Dtos
{
    public class SelectedArea
    {
        public int AreaId { get; set; }
        public string AreaName { get; set; }
        public string Note { get; set; }
        public string GuestName { get; set; }
        public bool IsSelected { get; set; }
        public DateTime DateOfBooking { get; set; }

        public int Ranking { get; set; }
        public bool Closed { get; set; }
        public bool HasBooked { get; set; }



    }
}
