using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Models
{
    public class GuestEvent
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

        [Column(TypeName = "decimal(18,2)")]
        public decimal? Deposit { get; set; }
        public DateTime? DateOfDeposit { get; set; } = DateTime.UtcNow;

        public DateTime DateOfEvent { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal PaymentCash { get; set; } = 0;
        [Column(TypeName = "decimal(18,2)")]
        public decimal PaymentVisa { get; set; } = 0;
      
        [Column(TypeName = "decimal(18,2)")]
        public decimal? LeftToPay { get; set; } = 0;

        [Column(TypeName = "decimal(18,2)")]
        public decimal EventPrice { get; set; } = 0;

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

        [ForeignKey("DepositWay")]
        public int? DepositWayId { get; set; }
        public DepositWay DepositWay { get; set; }

        [ForeignKey("Event")]
        public int EventId { get; set; }
        public Event Event { get; set; }

    }
}
