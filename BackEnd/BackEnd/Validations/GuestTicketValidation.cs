using BackEnd.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Validations
{
    public class GuestTicketValidation :AbstractValidator<GuestTicket>
    {
        public GuestTicketValidation()
        {
            RuleFor(c => c.GuestId).NotEmpty().NotNull().WithMessage("Guest Id Must not be null ");
            RuleFor(c => c.PriceForKids).GreaterThan(-1).WithMessage("Price For Kids Musn't be null Or Less Than 0");
            RuleFor(c => c.PriceForAdult).GreaterThan(-1).WithMessage("Price For Adult Musn't be null Or Less Than 0");
            RuleFor(c => c.TicketId).NotEmpty().NotNull().WithMessage("Ticket Id Must not be null");
            RuleFor(c => c.CountOfAdult).GreaterThan(-1).WithMessage("Count of Adult Must not be null Or less than 0");
            RuleFor(c => c.CountOfKids).GreaterThan(-1).WithMessage("Count of Kids Must not be null Or less than 0");
            RuleFor(c => c.DateOfBooking).NotEmpty().NotNull().WithMessage("Date Of Booking Must not be null ");


        }
    }
}
