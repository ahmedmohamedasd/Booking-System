using BackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.ViewModel
{
    public class AddActivityViewModel
    {
       public Guest GuestInfo { get;set; }
       public List<ActivityModel> SelectedActivities { get; set; }
        public List<BookedGuestArea> SelectedArea { get; set; }

    }
}
