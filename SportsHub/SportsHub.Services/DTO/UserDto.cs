using Microsoft.AspNetCore.Identity;

namespace SportsHub.Services.DTO
{
    public class UserDto : IdentityUser
    {
        public string Name { get; set; }
        public string SurName { get; set; }
        public string ImagePath { get; set; }
        public string Location { get; set; }
        public int? SportKindId { get; set; }
        public SportKindDto SportKind { get; set; }
        public int? TrainingProgramId { get; set; }
        public TrainingProgramDto TrainingProgram { get; set; }
    }
}
