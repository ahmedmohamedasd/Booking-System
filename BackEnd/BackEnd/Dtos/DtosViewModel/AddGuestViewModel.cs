using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Dtos.DtosViewModel
{
    public class AddGuestViewModel
    {
        public GuestInfo GuestInfo { get; set; }
        public List<ListOfTicket> ListOfTicket { get; set; }
        public List<SelectedArea> SelectedArea { get; set; }
    }
}
