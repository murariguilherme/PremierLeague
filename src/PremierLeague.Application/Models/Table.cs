using System;
using System.Collections.Generic;
using System.Linq;

namespace PremierLeague.Application.Models
{
    public class Table
    {
        public List<Team> Teams { get; set; } = new List<Team>();
        public List<RoundMatchs> RoundMatchs { get; set; } = new List<RoundMatchs>();
        public void GenerateMatchs()
        {
            var firstBlockTeam = Teams.GetRange(0, 10).ToList();
            var secondBlockTeam = Teams.GetRange(10, 10).ToList();
            
            var matchList = new List<Match>();           

            for (var i = 1; i <= 18; i++)
            {
                for (var roundNumber = 0; roundNumber <= 9; roundNumber++)
                {                    
                    var match = GenerateMatch(firstBlockTeam[roundNumber], secondBlockTeam[roundNumber]);
                    matchList.Add(match);                    
                }

                var roundMatch = new RoundMatchs()
                {
                    RoundNumber = i,
                    Matchs = matchList.ToList()
                };

                RoundMatchs.Add(roundMatch);

                ChangeArrayPosition(firstBlockTeam, secondBlockTeam);
                matchList.Clear();
            }

            var returno = GenerateMatchBack(RoundMatchs);
            RoundMatchs.AddRange(returno);
        }

        private Match GenerateMatch(Team firstTeam, Team secondTeam)
        {
            Match match;
            var randomNumber = new Random().Next(1, 3);
            
            if (randomNumber == 1)
                match = new Match(firstTeam, secondTeam);
            else
                match = new Match(secondTeam, firstTeam);

            return match;
        }

        private void ChangeArrayPosition(List<Team> firstBlockTeam, List<Team> secondBlockTeam)
        {
            var last = firstBlockTeam[firstBlockTeam.Count - 1];
            var first = firstBlockTeam[0];
            firstBlockTeam[0] = last;
            firstBlockTeam[firstBlockTeam.Count - 1] = first;
        }

        private List<RoundMatchs> GenerateMatchBack(List<RoundMatchs> turno)
        {
            var changeHome = new List<RoundMatchs>();
            var matchslist = new List<Match>();
            var i = turno.Count + 1;
            foreach (var round in turno)
            {
                foreach (var match in round.Matchs)
                {
                    matchslist.Add(new Match(match.Opposite, match.Home));
                }
                changeHome.Add(new RoundMatchs() { Matchs = matchslist.ToList(), RoundNumber = i });
                i++;
                matchslist.Clear();
            }            
            return changeHome;
        }
        public void GenerateStandings () 
        {
            var standings = Teams
                .OrderByDescending(t => t.TeamStatus.TotalPoints)
                .ThenByDescending(t => t.TeamStatus.Victorys)
                .ThenByDescending(t => t.TeamStatus.GoalsScored)
                .ToList();

            standings.ForEach(t => {
                Console.WriteLine($"{standings.IndexOf(t) + 1}- {t.Name}, {t.TeamStatus.TotalPoints} pts, {t.TeamStatus.Victorys} V, {t.TeamStatus.GoalsScored} GS");
            });
        }
    }
}
