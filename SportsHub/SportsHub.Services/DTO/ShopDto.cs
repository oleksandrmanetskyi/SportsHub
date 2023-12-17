namespace SportsHub.Services.DTO
{
    public class ShopDto
    {
        public int Id { get; set; }
        public string Location { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public int SportKindId { get; set; }
        public SportKindDto SportKind { get; set; }
    }
}
