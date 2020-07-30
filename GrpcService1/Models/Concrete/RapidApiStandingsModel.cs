using static GrpcService1.CurrentStandingsResponse.Types;

namespace GrpcService1.Models.Concrete
{
    public class RapidApiStandingsModel
    {
        public Api api { get; set; }

        public class Api
        {
            public int results { get; set; }
            public RapidApiStanding[][] standings { get; set; }

            public class RapidApiStanding
            {
                public int rank { get; set; }
                public int team_id { get; set; }
                public string teamName { get; set; }
                public string logo { get; set; }
                public string group { get; set; }
                public string forme { get; set; }
                public object status { get; set; }
                public string description { get; set; }
                public All all { get; set; }
                public Home home { get; set; }
                public Away away { get; set; }
                public int goalsDiff { get; set; }
                public int points { get; set; }
                public string lastUpdate { get; set; }

                public static explicit operator Standing(RapidApiStanding b) => new Standing
                {
                    TeamName = b.teamName,
                    Rank = b.rank,
                    MatchesPlayed = b.all.matchsPlayed,
                    Win = b.all.win,
                    Draw = b.all.draw,
                    Lose = b.all.lose,
                    GoalsFor = b.all.goalsFor,
                    GoalsAgainst = b.all.goalsAgainst,
                    GoalsDiff = b.goalsDiff,
                    Points = b.points
                };

                public class All
                {
                    public int matchsPlayed { get; set; }
                    public int win { get; set; }
                    public int draw { get; set; }
                    public int lose { get; set; }
                    public int goalsFor { get; set; }
                    public int goalsAgainst { get; set; }
                }

                public class Home
                {
                    public int matchsPlayed { get; set; }
                    public int win { get; set; }
                    public int draw { get; set; }
                    public int lose { get; set; }
                    public int goalsFor { get; set; }
                    public int goalsAgainst { get; set; }
                }

                public class Away
                {
                    public int matchsPlayed { get; set; }
                    public int win { get; set; }
                    public int draw { get; set; }
                    public int lose { get; set; }
                    public int goalsFor { get; set; }
                    public int goalsAgainst { get; set; }
                }
            }
        }
    }
}