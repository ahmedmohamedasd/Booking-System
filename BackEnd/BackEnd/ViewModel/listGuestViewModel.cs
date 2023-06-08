using BackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.ViewModel
{
    public class listGuestViewModel
    {
        public Guest Guests { get; set; }
        public IEnumerable<GuestActivity> GuestActivities { get; set; }
        public IEnumerable<GuestTicket> GuestTickets { get; set; }
        public IEnumerable<BookedGuestArea> GuestAreas { get; set; }

    }
}
