namespace secondProject.Dtos
{
    public class UpdatePasswordDto
    {
        public string Token { get; set; }
        public string Email{ get; set; }
        public string  Password{ get; set; }
    }
}
