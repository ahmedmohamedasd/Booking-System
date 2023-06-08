using BackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.ViewModel
{
    public class TicketViewModel
    {
        public List<Ticket> Tickets { get; set; }
        public List<DepositWay> DepositWays { get; set; }
        public List<WhereYouFrom> WhereYouFroms { get; set; }
        public List<HowYouKnowUs> HowYouKnowUs { get; set; }

    }
}
