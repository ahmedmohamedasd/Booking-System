using BackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.ViewModel
{
    public class EditSelectedAreaViewModel
    {
        public List<BookedGuestArea> BookedGuestAreas { get; set; }
        public DateTime CurrentDate { get; set; }
    }
}
