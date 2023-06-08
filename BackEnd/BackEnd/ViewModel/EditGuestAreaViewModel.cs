using BackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.ViewModel
{
    public class EditGuestAreaViewModel
    {
        public List<AreaViewModel> NewBooking { get; set; }
        public List<BookedGuestArea> OldBooking { get; set; }
    }
}
