using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Models
{
    public class Guest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Phone { get; set; }
        public string? Identifier { get; set; }
        public string? Email { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? DiscountByPercentage { get; set; } = 0;
        [Column(TypeName = "decimal(18,2)")]
        public decimal? DiscountByAmount { get; set; } = 0;
        [Column(TypeName ="decimal(18,2)")]
        public decimal? Deposit { get; set; }
        public DateTime? DateOfDeposit { get; set; } = DateTime.UtcNow;
        public DateTime DateOfBooking { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal PaymentCash { get; set; } = 0;
        [Column(TypeName = "decimal(18,2)")]
        public decimal PaymentVisa { get; set; } = 0;
        [Column(TypeName = "decimal(18,2)")]
        public decimal DebitNote { get; set; } = 0;
        [Column(TypeName = "decimal(18,2)")]
        public decimal TaxIncluded { get; set; } = 14;
        [Column(TypeName = "decimal(18,2)")]
        public decimal LeftToPay { get; set; } = 0;

        [Column(TypeName = "decimal(18,2)")]
        public decimal GrandTotal { get; set; } = 0;

        public int TotalCountOfguest { get; set; }
        public bool? IsCanceled { get; set; } = false;

        //Foreign Key Elements
        [ForeignKey("HowYouKnowUs")]
        public int? KnowUsId { get; set; }
        public HowYouKnowUs HowYouKnowUs { get; set; }

        [ForeignKey("WhereYouFrom")]
        public int? WhereYouId { get; set; }
        public WhereYouFrom WhereYouFrom { get; set; }

        [ForeignKey("BookingType")]
        public int BookingTypeId { get; set; }
        public BookingType BookingType { get; set; }
        
        [ForeignKey("DepositWay")]
        public int? DepositWayId { get; set; }
        public DepositWay DepositWay { get; set; }

        public ICollection<BookedGuestArea> BookedGuestAreas { get; set; }
        public ICollection<GuestActivity> GuestActivities { get; set; }
        public ICollection<GuestTicket> GuestTickets { get; set; }


    }
}
