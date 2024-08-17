namespace secondProject.Dtos.ThingsToDoDtos
{
    public class GetThingsToDoDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Price { get; set; }
        public List<string> ImageUrls { get; set; } = new List<string>();
    }
}
