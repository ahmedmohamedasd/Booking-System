using BackEnd.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Validations
{
    public class BookedGuestAreaValidation :AbstractValidator<BookedGuestArea>
    {
        public BookedGuestAreaValidation()
        {
            RuleFor(c => c.AreaId).NotEmpty().NotNull().WithMessage("Area Id Must be Unique and not be null ");
            RuleFor(c => c.DateOfBooking).NotEmpty().NotNull().WithMessage("Date of Booking Must not be null Or less than 0");
            RuleFor(c => c.GuestId).NotEmpty().NotNull().WithMessage("Guest ID Must not be null Or less than 0");
        }
    }
}
