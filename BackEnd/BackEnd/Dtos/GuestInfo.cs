using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Dtos
{
    public class GuestInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Phone { get; set; }
        public string? Identifier { get; set; }
        public string? Email { get; set; }
        public decimal? DiscountByPercentage { get; set; } = 0;
        public decimal? DiscountByAmount { get; set; } = 0;
        public decimal? Deposit { get; set; }
        public DateTime DateOfDeposit { get; set; } = DateTime.UtcNow;
        public DateTime DateOfBooking { get; set; }
        public decimal PaymentCash { get; set; } = 0;
        public decimal PaymentVisa { get; set; } = 0;
        public decimal DebitNote { get; set; } = 0;
        public decimal TaxIncluded { get; set; } = 14;
        public decimal LeftToPay { get; set; } = 0;
        public decimal GrandTotal { get; set; } = 0;
        public int TotalCountOfguest { get; set; }
        public int? KnowUsId { get; set; }
        public int? WhereYouId { get; set; }
        public int BookingTypeId { get; set; }
        public int? DepositWayId { get; set; }


        //Another Prop
        public string NewPlaceName { get; set; }
        public string NewSocialName { get; set; }
        public int QuantityForAdult { get; set; }
        public int QuantityFoeKids { get; set; }




    }
}
