using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Dtos
{
    public class BlockAreaDto
    {
        public int Id { get; set; }      
        public DateTime DateOfBlock { get; set; }
        public string? Note { get; set; }
        public bool IsBlocked { get; set; }
        public int AreaId { get; set; }
    }
}
