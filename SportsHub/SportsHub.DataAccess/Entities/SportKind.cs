using System.Collections.Generic;

namespace SportsHub.DataAccess.Entities
{
    public class SportKind
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<User> Users { get; set; }
        public IEnumerable<News> News { get; set; }
        public IEnumerable<SportPlace> SportPlaces { get; set; }
        public IEnumerable<Shop> Shops { get; set; }
        public IEnumerable<TrainingProgram> TrainingPrograms { get; set; }
    }
}
