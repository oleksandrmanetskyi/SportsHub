using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsHub.RecommendationSystem.DatabaseSeeding.FileParsers
{
    public class GenreMapper
    {

        private Dictionary<string, int> sportKindGenreMappings = new Dictionary<string, int>();
        private SportsHubDbContext _db;
        public GenreMapper(SportsHubDbContext db, List<string> genres)
        {
            _db = db;
            var kinds = _db.SportKinds.ToList();
            int count = Math.Min(kinds.Count(), genres.Count);
            for (int i = 0; i < count; i++)
            {
                sportKindGenreMappings.Add(genres[i], kinds[i].Id);
            }
        }

        public int MapGenreToSportKind(string genre)
        {
            return sportKindGenreMappings[genre];
        }

    }
}
