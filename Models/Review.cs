using System.ComponentModel.DataAnnotations;

namespace secondProject.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }
        public int? Rating { get; set; }
        public string? Comment { get; set; }

        // foreign key to hotel
        public int? HotelId { get; set; }

        // hotel navigation : Many review belongs to one hotel
        public Hotel? Hotel { get; set; }

        public int? userId { get; set; }
        public User? User { get; set; }

    }
}
