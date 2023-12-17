namespace SportsHub.DataAccess.Entities
{
    public class News
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public int SportKindId { get; set; }
        public SportKind SportKind { get; set; }
    }
}
