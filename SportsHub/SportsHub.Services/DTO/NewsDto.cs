namespace SportsHub.Services.DTO
{
    public class NewsDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public int SportKindId { get; set; }
        public SportKindDto SportKind { get; set; }
    }
}
