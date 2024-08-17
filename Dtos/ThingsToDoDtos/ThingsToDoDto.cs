namespace secondProject.Dtos.ThingsToDoDtos
{
    public class ThingsToDoDto
    {
        public string? Name { get; set; }
        public int Price { get; set; }
        public List<string> ImageUrls { get; set; } = new List<string>();
    }
}
