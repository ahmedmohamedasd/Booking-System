using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Models
{
    public class BlockArea
    {
        public int Id { get; set; }
        [DataType(DataType.Date)]
        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime DateOfBlock { get; set; }
        public string? Note { get; set; }
        public bool IsBlocked { get; set; } = false;
        [ForeignKey("Area")]
        public int AreaId { get; set; }
        public Area Area { get; set; }
    }
}
