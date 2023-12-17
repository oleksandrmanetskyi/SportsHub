using Dapper;
using Microsoft.Data.SqlClient;
using SportsHub.RecommendationSystem.DatabaseSeeding;
using SportsHub.RecommendationSystem.Services.RecommendationModule.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsHub.RecommendationSystem.Services.Database
{
    public class SportsHubRepository : ISportsHubRepository
    {
        private string connectionString;

        public SportsHubRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }
         
        public List<string> GetAllUserIds()
        {
            var sql = "select Id from dbo.AspNetUsers";
            var users = new List<string>();
            using (var connection = new SqlConnection(this.connectionString))
            {
                connection.Open();
                users = connection.Query<string>(sql).ToList();
            }
            return users;
        }

        public List<int> GetAllTrainingProgramIds()
        {
            var sql = "select Id from dbo.TrainingPrograms";
            var ids = new List<int>();
            using (var connection = new SqlConnection(this.connectionString))
            {
                connection.Open();
                ids = connection.Query<int>(sql).ToList();
            }
            return ids;
        }

        public List<SuggestionDto> GetAllTrainingProgramsFromRecommendations()
        {
            var sql = "select TrainingProgramId, UserId, ScoreAssumption from dbo.[Recommendations]";
            var ids = new List<SuggestionDto>();
            using (var connection = new SqlConnection(this.connectionString))
            {
                connection.Open();
                ids = connection.Query<SuggestionDto>(sql).ToList();
            }
            return ids;
        }
    }
}
