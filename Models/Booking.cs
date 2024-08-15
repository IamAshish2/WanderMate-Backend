using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace secondProject.Models
{
    public class Booking
    {
        [Key]
        public int BookingId { get; set; }
        public DateTime BookingDate { get; set; }
        public int? UserId { get; set; }   
        public User? User { get; set; }

        // hotel id foreign key
        public int? HotelId { get; set; }
        public Hotel? Hotel { get; set; }


    }
}