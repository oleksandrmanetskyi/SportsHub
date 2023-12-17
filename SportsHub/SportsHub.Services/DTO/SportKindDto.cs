using System.Collections.Generic;

namespace SportsHub.Services.DTO
{
    public class SportKindDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<UserDto> Users { get; set; }
        public IEnumerable<NewsDto> News { get; set; }
        public IEnumerable<SportPlaceDto> SportPlaces { get; set; }
        public IEnumerable<ShopDto> Shops { get; set; }
        public IEnumerable<TrainingProgramDto> TrainingPrograms { get; set; }
    }
}
