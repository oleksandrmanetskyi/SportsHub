using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsHub.RecommendationSystem.DatabaseSeeding.FileParsers
{
    public class SCVParser
    {
        public static Dictionary<string, string[]> ParseMoviesCSV(string filePath)
        {
            Dictionary<string, string[]> seriesData = new Dictionary<string, string[]>();

            var conf = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                Delimiter = ",",
                TrimOptions = TrimOptions.Trim,
            };
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, conf))
            {
                csv.Context.RegisterClassMap<SeriesMap>();

                while (csv.Read())
                {
                    var series = csv.GetRecord<Series>();
                    seriesData[series.Series_Title] = new string[] { series.Genre, series.Overview };
                }
            }

            return seriesData;
        }
        public static Dictionary<string, string> ParseNetflixMoviesCSV(string filePath)
        {
            Dictionary<string, string> seriesData = new Dictionary<string, string>();

            using (StreamReader reader = new StreamReader(filePath))
            {
                // Читаємо перший рядок з заголовками полів
                string[] headers = reader.ReadLine().Split(',');

                while (!reader.EndOfStream)
                {
                    string[] values = reader.ReadLine().Split(',');

                    // Отримуємо індекси потрібних полів
                    int idIndex = Array.IndexOf(headers, "Movie_ID");
                    int nameIndex = Array.IndexOf(headers, "Name");


                    // Отримуємо значення потрібних полів
                    string id = values[idIndex];
                    string name = values[nameIndex];

                    // Записуємо значення у словник
                    seriesData[name] = id;
                }
            }

            return seriesData;
        }

        internal static List<string[]> ParseRatingsCSV(string path, Predicate<string[]> action)
        {
            List<string[]> seriesData = new List<string[]>();

            using (StreamReader reader = new StreamReader(path))
            {
                // Читаємо перший рядок з заголовками полів
                string[] headers = reader.ReadLine().Split(',');

                while (!reader.EndOfStream)
                {
                    string[] values = reader.ReadLine().Split(',');

                    // Отримуємо індекси потрібних полів
                    int userIdIndex = Array.IndexOf(headers, "User_ID");
                    int ratingIndex = Array.IndexOf(headers, "Rating");
                    int movieIdIndex = Array.IndexOf(headers, "Movie_ID");
                 

                    // Отримуємо значення потрібних полів
                    string userId = values[userIdIndex];
                    string rating = values[ratingIndex];
                    string movieId = values[movieIdIndex];
                    var res = new string[] { userId, rating, movieId };
                    if (action(res))
                    {
                        // Записуємо значення у словник
                        seriesData.Add(res);
                    }
                    
                }
            }

            return seriesData;
        }
    }

    


    public class Series
    {
        public string Poster_Link { get; set; }
        public string Series_Title { get; set; }
        public string Released_Year { get; set; }
        public string Certificate { get; set; }
        public string Runtime { get; set; }
        public string Genre { get; set; }
        public string IMDB_Rating { get; set; }
        public string Overview { get; set; }
        public string Meta_score { get; set; }
        public string Director { get; set; }
        public string Star1 { get; set; }
        public string Star2 { get; set; }
        public string Star3 { get; set; }
        public string Star4 { get; set; }
        public string No_of_Votes { get; set; }
        public string Gross { get; set; }
    }

    public sealed class SeriesMap : ClassMap<Series>
    {
        public SeriesMap()
        {
            Map(m => m.Poster_Link).Name("Poster_Link");
            Map(m => m.Series_Title).Name("Series_Title");
            Map(m => m.Released_Year).Name("Released_Year");
            Map(m => m.Certificate).Name("Certificate");
            Map(m => m.Runtime).Name("Runtime");
            Map(m => m.Genre).Name("Genre");
            Map(m => m.IMDB_Rating).Name("IMDB_Rating");
            Map(m => m.Overview).Name("Overview");
            Map(m => m.Meta_score).Name("Meta_score");
            Map(m => m.Director).Name("Director");
            Map(m => m.Star1).Name("Star1");
            Map(m => m.Star2).Name("Star2");
            Map(m => m.Star3).Name("Star3");
            Map(m => m.Star4).Name("Star4");
            Map(m => m.No_of_Votes).Name("No_of_Votes");
            Map(m => m.Gross).Name("Gross");
        }
    }
}
