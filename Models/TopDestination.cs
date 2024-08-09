using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace secondProject.Models
{
    public class Destination
    {
        public int Id { get; set; }
        public string?  Name { get; set; }
        public string? Description { get; set; }
        public int Price { get; set; }
        public List<string> ImageUrls { get; set; } = new List<string>();
    }
}