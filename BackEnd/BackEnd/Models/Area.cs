using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Models
{
    public class Area
    {
        [Key]
        public int Id { get; set; }
        public string AreaName { get; set; }
        public int AreaRanking { get; set; }
        public int AreaMinimum { get; set; }
        public ICollection<BookedGuestArea> BookedGuestAreas { get; set; }

    }
}
