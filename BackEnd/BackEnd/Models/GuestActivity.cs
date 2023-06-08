using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Models
{
    public class GuestActivity
    {
        [ForeignKey("Guest")]
        public int GuestId { get; set; }
        public Guest Guest { get; set; }

        [ForeignKey("Activity")]
        public int ActivityId { get; set; }
        public Activity Activity { get; set; }

        public int Quantity { get; set; } = 0;

        [Column(TypeName ="decimal(18,2)")]
        public decimal SubTotal { get; set; } = 0;

        public bool IsIncluded { get; set; } = false;

        [DataType(DataType.Date)]
        public DateTime DateOfBooking { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? ActivityPrice { get; set; }

    }
}
