using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Dtos
{
    public class GuestDtos
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Phone { get; set; }
        public string? Identifier { get; set; }
        public string? Email { get; set; }
        public decimal? DiscountByPercentage { get; set; } 
        public decimal? DiscountByAmount { get; set; } 
        public decimal? Deposit { get; set; }
        public DateTime DateOfDeposit { get; set; } 
        public DateTime DateOfBooking { get; set; }
        public decimal PaymentCash { get; set; } 
        public decimal PaymentVisa { get; set; } 
        public decimal DebitNote { get; set; } 
        public decimal TaxIncluded { get; set; }
        public decimal LeftToPay { get; set; }
        public decimal GrandTotal { get; set; } 
        public int TotalCountOfguest { get; set; }
        public int? KnowUsId { get; set; }
        public int? WhereYouId { get; set; }
        public int BookingTypeId { get; set; }
        public int? DepositWayId { get; set; }
    }
}
