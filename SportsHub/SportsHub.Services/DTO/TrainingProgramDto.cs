namespace SportsHub.Services.DTO
{
    public class TrainingProgramDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public int? NutritionId { get; set; }
        public NutritionDto Nutrition { get; set; }
        public int? SportKindId { get; set; }
        public SportKindDto SportKind { get; set; }
        public UserDto User { get; set; }
    }
}
