using System;
using System.Collections.Generic;
using System.IO;
using KBCsv;
using System.Text.Json;

namespace PT_Lab1
{
    public class CSVReader
    {
        public static List<Credit> ReadCredits(string path)
        {
            List<Credit> res = new List<Credit>();
            using (var streamReader = new StreamReader(path))
            using (var csvReader = new CsvReader(streamReader))
            {
                csvReader.ReadHeaderRecord();

                while (csvReader.HasMoreRecords)
                {
                    var record = csvReader.ReadDataRecord();
                    res.Add(new Credit(Convert.ToInt32(record["movie_id"]), record["title"],
                        JsonSerializer.Deserialize<List<CastMember>>(record["cast"]),
                        JsonSerializer.Deserialize<List<CrewMember>>(record["crew"])));
                }
            }

            return res;
        }

        public static List<Movie> ReadMovies(string path)
        {
            List<Movie> res = new List<Movie>();
            using (var streamReader = new StreamReader(path))
            using (var csvReader = new CsvReader(streamReader))
            {
                csvReader.ReadHeaderRecord();

                while (csvReader.HasMoreRecords)
                {
                    var record = csvReader.ReadDataRecord();
                    res.Add(new Movie(Convert.ToInt64(record["budget"]),
                        JsonSerializer.Deserialize<List<Genre>>(record["genres"]), record["homepage"],
                        Convert.ToInt64(record["id"]), JsonSerializer.Deserialize<List<Keyword>>(record["keywords"]),
                        record["original_language"], record["original_title"], record["overview"],
                        Convert.ToDouble(record["popularity"]),
                        JsonSerializer.Deserialize<List<Company>>(record["production_companies"]),
                        JsonSerializer.Deserialize<List<Country>>(record["production_countries"]),
                        record["release_date"], Convert.ToInt64(record["revenue"]),
                        record["runtime"] == "" ? 0 : Convert.ToDouble(record["runtime"]),
                        JsonSerializer.Deserialize<List<Language>>(record["spoken_languages"]), record["status"],
                        record["tagline"], record["title"], Convert.ToDouble(record["vote_average"]),
                        Convert.ToInt64(record["vote_count"])));
                }
            }

            return res;
        }
    }
}