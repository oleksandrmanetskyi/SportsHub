namespace SportsHub.Services.DTO
{
    public class NutritionDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public TrainingProgramDto TrainingProgram { get; set; }
    }
}
