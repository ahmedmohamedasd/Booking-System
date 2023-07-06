using BackEnd.Migrations;
using BackEnd.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GuestEvent = BackEnd.Models.GuestEvent;

namespace BackEnd.Validations
{
    public class GuestEventValidation :AbstractValidator<GuestEvent>
    {
        public GuestEventValidation()
        {
            RuleFor(u => u.Name).NotEmpty().NotNull();
            RuleFor(c => c.DateOfEvent).NotNull().NotEmpty();
            RuleFor(c => c.Deposit).NotNull().NotEmpty().GreaterThan(-1);
            RuleFor(c => c.DepositWayId).NotEmpty().NotNull();
            RuleFor(c => c.DiscountByAmount).NotEmpty().NotNull();
            RuleFor(c => c.DiscountByPercentage).NotEmpty().NotNull();
            RuleFor(c => c.EventId).NotEmpty().NotNull();
            RuleFor(c => c.EventPrice).NotEmpty().NotNull();
            RuleFor(c => c.KnowUsId).NotEmpty().NotNull();
            RuleFor(c => c.Phone).NotEmpty().NotNull();
            RuleFor(c => c.TotalCountOfguest).NotEmpty().NotNull();
            RuleFor(c => c.WhereYouId).NotEmpty().NotNull();
            RuleFor(c => c.PaymentVisa).GreaterThan(-1).WithMessage("Payment Visa Shoud Be Equal Zero Or More");
            RuleFor(c => c.PaymentCash).GreaterThan(-1);
            RuleFor(c => c.GrandTotal).GreaterThan(-1);

        }
    }
}
