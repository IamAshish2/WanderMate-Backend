using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace secondProject.Dtos.HotelDTOs
{
    public class GetHotelDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

        public string? Address { get; set; }
        public int? Price { get; set; }
        public bool FreeCancellation { get; set; } = false;
        public bool ReserveNow { get; set; } = false;
        public List<string> ImageUrl { get; set; } = new List<string>();
    }
}