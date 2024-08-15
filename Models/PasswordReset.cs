using System.ComponentModel.DataAnnotations;

namespace secondProject.Models
{
    public class PasswordReset
    {
        [Key]
        public int Id{ get; set; }
        public string Token { get; set; }
    }
}
