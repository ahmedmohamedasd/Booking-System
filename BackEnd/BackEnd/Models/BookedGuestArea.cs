using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Models
{
    public class BookedGuestArea
    {
        [ForeignKey("Guest")]
        public int GuestId { get; set; }
        public Guest Guest { get; set; }

        [ForeignKey("Area")]
        public int AreaId { get; set; }
        public Area Area { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateOfBooking { get; set; }

      
    }
}
