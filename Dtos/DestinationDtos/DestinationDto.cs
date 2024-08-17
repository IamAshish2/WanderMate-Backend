namespace secondProject.Dtos.DestinationDtos
{
    public class DestinationDto
    {
        public string? Name { get; set; }
        public int Price { get; set; }
        public List<string> ImageUrls { get; set; } = new List<string>();
        public string? Description { get; set; }
    }
}
