using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using secondProject.Migrations;

namespace secondProject.Models
{
    public class Hotel
    {
        [Key]
         public int Id { get; set; }
        
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string ImageUrl { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public int? Price{ get; set; } 

        public bool IsDeleted{get;set;} = false;

        public ICollection<Reviews>  Reviews { get; set; } = new List<Reviews>();

      
    }
}
