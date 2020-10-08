using System.Collections.Generic;

namespace PT_Lab1
{
    public class Credit
    {
        public Credit(int movieId, string title, List<CastMember> cast, List<CrewMember> crew)
        {
            MovieId = movieId;
            Title = title;
            Cast = cast;
            Crew = crew;
        }

        public int MovieId { get; set; }
        public string Title { get; set; }
        public List<CastMember> Cast { get; set; }
        public List<CrewMember> Crew { get; set; }
    }
}