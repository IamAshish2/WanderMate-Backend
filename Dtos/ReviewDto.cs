using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

namespace secondProject.Dtos
{
    public class ReviewDto
    {
        public int? Rating{get; set;}
        public string? Comment { get; set; }
        public int? HotelId { get; set; }
    }
}