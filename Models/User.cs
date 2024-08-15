using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;


namespace secondProject.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string? Email { get; set; }
        public string Role { get; set; }
        public string Password { get; set; }
        public string? ConformPassword{ get; set; }

        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();

    }
}