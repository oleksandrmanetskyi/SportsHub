namespace SportsHub.DataAccess.Entities
{
    public class Nutrition
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public TrainingProgram TrainingProgram { get; set; }
    }
}
