using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace secondProject.Models

{
    public class TravelPackages
    {
        [Key]
         public int Id { get; set; }
        public string? Name { get; set; }
        //public string? ImageUrl { get; set; }
        public List<string> ImageUrl { get; set; } = new List<string>();
        public string? Description { get; set; }
        public bool FreeCancellation { get; set; } = false;
        public bool ReserveNow { get; set; } = false;
    }
}
