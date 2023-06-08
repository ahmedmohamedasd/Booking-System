using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Dtos.DtosViewModel
{
    public class AddActivityViewModelDtos
    {
        public GuestInfo GuestInfo { get; set; }
        public List<ActivityModelDtos> SelectedActivities { get; set; }
        public List<SelectedArea> SelectedArea { get; set; }
    }
}
