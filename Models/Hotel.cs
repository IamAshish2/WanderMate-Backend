using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
//using secondProject.Migrations;

namespace secondProject.Models
{
    public class Hotel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int? Price { get; set; }
        public bool FreeCancellation { get; set; } = false;
        public bool ReserveNow { get; set; } = false;
        public List<string> ImageUrl { get; set; } = new List<string>();

        //public bool IsDeleted { get; set; } = false;
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
