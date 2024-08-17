using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace secondProject.Models
{
    public class ThingsToDo
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Price { get; set; }
        public List<string> ImageUrls { get; set; } = new List<string>();
    }
}