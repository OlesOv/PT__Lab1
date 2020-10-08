using System;
using System.Collections.Generic;

namespace PT_Lab1
{
    public class Movie
    {
        public Movie(Int64 budget, List<Genre> genres, string homepage, Int64 id, List<Keyword> keywords,
            string originalLanguage, string originalTitle, string overview, double popularity,
            List<Company> productionCompanies, List<Country> productionCountries, string releaseDate, Int64 revenue,
            double runtime,
            List<Language> spokenLanguages, string status, string tagline, string title, double voteAverage,
            Int64 voteCount)
        {
            this.budget = budget;
            this.genres = genres;
            this.homepage = homepage;
            this.id = id;
            this.keywords = keywords;
            original_language = originalLanguage;
            original_title = originalTitle;
            this.overview = overview;
            this.popularity = popularity;
            production_companies = productionCompanies;
            production_countries = productionCountries;
            if (releaseDate.Length > 0)
            {
                var year = Convert.ToInt32(releaseDate.Substring(0, releaseDate.IndexOf('-')));
                var month = Convert.ToInt32(releaseDate.Substring(releaseDate.IndexOf('-') + 1,
                    releaseDate.IndexOf('-', releaseDate.IndexOf('-') + 1) - releaseDate.IndexOf('-') - 1));
                var day = Convert.ToInt32(releaseDate.Substring(
                    releaseDate.IndexOf('-', releaseDate.IndexOf('-') + 1) + 1,
                    releaseDate.Length - releaseDate.IndexOf('-', releaseDate.IndexOf('-') + 1) - 1));
                release_date = new DateTime(year, month, day);
            }
            else release_date = new DateTime(0);

            this.revenue = revenue;
            this.runtime = runtime;
            spoken_languages = spokenLanguages;
            this.status = status;
            this.tagline = tagline;
            this.title = title;
            vote_average = voteAverage;
            vote_count = voteCount;
        }

        public Int64 budget { get; set; }
        public List<Genre> genres { get; set; }
        public string homepage { get; set; }
        public Int64 id { get; set; }
        public List<Keyword> keywords { get; set; }
        public string original_language { get; set; }
        public string original_title { get; set; }
        public string overview { get; set; }
        public double popularity { get; set; }
        public List<Company> production_companies { get; set; }
        public List<Country> production_countries { get; set; }
        public DateTime release_date { get; set; }
        public Int64 revenue { get; set; }
        public double runtime { get; set; }
        public List<Language> spoken_languages { get; set; }
        public string status { get; set; }
        public string tagline { get; set; }
        public string title { get; set; }
        public double vote_average { get; set; }
        public Int64 vote_count { get; set; }
    }
}