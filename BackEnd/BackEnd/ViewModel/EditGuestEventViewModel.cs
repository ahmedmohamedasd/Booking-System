using BackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.ViewModel
{
    public class EditGuestEventViewModel
    {
        public List<WhereYouFrom> WhereYouFrom { get; set; }
        public List<HowYouKnowUs> HowYouKnowUs { get; set; }
        public List<DepositWay> DepositWays { get; set; }
        public List<Event> Events { get; set; }
        public GuestEvent GuestEventInfo { get; set; }
    }
}
