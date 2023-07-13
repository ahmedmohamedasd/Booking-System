using BackEnd.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Validations
{
    public class GuestValidation :AbstractValidator<Guest>
    {
        public GuestValidation()
        {
            RuleFor(u => u.Name).NotEmpty().NotNull();
            RuleFor(c => c.DateOfBooking).NotNull().NotEmpty();
            RuleFor(c => c.Deposit).NotNull().NotEmpty().GreaterThan(-1);
            RuleFor(c => c.DepositWayId).NotEmpty().NotNull();
            RuleFor(c => c.DiscountByAmount).NotEmpty().NotNull().GreaterThan(-1);
            RuleFor(c => c.DiscountByPercentage).NotEmpty().NotNull().GreaterThan(-1);
            RuleFor(c => c.KnowUsId).NotEmpty().NotNull();
            RuleFor(c => c.Phone).NotEmpty().NotNull();
            RuleFor(c => c.TotalCountOfguest).NotEmpty().NotNull().GreaterThan(0);
            RuleFor(c => c.WhereYouId).NotEmpty().NotNull();
            RuleFor(c => c.PaymentVisa).GreaterThan(-1).WithMessage("Payment Visa Shoud Be Equal Zero Or More");
            RuleFor(c => c.PaymentCash).GreaterThan(-1).WithMessage("Payment Cash Shoud Be Equal Zero Or More");
            RuleFor(c => c.GrandTotal).GreaterThan(-1);
            RuleFor(c => c.DebitNote).GreaterThan(-1);
            RuleFor(c => c.LeftToPay).GreaterThan(-1);
            RuleFor(c => c.GrandTotal).GreaterThan(-1);
            RuleFor(c => c.BookingTypeId).NotEmpty().NotNull();
        }
    }
}
