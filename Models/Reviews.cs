using System.ComponentModel.DataAnnotations;

namespace secondProject.Models
{
    public class Reviews
    {
        [Key]
        public int Id { get; set; }
        public int Rating { get; set; }
        public string? comment { get; set; }

        // foreign key to hotel
        public int? HotelId { get; set; }
        
        // hotel navigation : A review belongs to one hotel
        public Hotel? Hotel  { get; set; }

    }
}
