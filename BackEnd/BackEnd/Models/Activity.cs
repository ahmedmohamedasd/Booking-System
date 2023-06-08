using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Models
{
    public class Activity
    {
        public int Id { get; set; }
        [Required]
        public string ActivityName { get; set; }
        [Column(TypeName="decimal(18,2)")]
        public decimal ActivityPrice { get; set; }
        public int Sorting { get; set; }
        public ICollection<GuestActivity> GuestActivities { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
