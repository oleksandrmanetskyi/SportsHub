namespace SportsHub.DataAccess.Entities
{
    public class SportPlace
    {
        public int Id { get; set; }
        public string Location { get; set; }
        public string Name { get; set; }
        public int SportKindId { get; set; }
        public SportKind SportKind { get; set; }
    }
}
