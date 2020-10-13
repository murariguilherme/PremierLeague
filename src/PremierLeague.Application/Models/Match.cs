using System;

namespace PremierLeague.Application.Models
{
    public class Match
    {
        public Team Home { get; set; }
        public int HomeGoals { get; set; }
        public Team Opposite { get; set; }
        public int OppositeGoals { get; set; }

        public Match(Team home, Team opposite)
        {
            Home = home;
            Opposite = opposite;
            HomeGoals = 0;
            OppositeGoals = 0;
        }
        public bool IsValid()
        {
            if (Home == null || Opposite == null) return false;
            if (HomeGoals < 0 || OppositeGoals < 0) return false;
            if (!Home.IsValid()) return false;
            if (!Opposite.IsValid()) return false;

            return true;
        }
        public bool MatchWasDraw() => HomeGoals == OppositeGoals;
        public Team? GetWinner()
        {
            if (MatchWasDraw()) return null;
            if (HomeGoals > OppositeGoals) return Home;
            return Opposite;
        }

        public Team? GetLoser()
        {
            if (MatchWasDraw()) return null;
            if (HomeGoals > OppositeGoals) return Opposite;
            return Home;
        }

        public void GenerateMatchResult () 
        {
            OppositeGoals = new Random().Next(5);
            HomeGoals = new Random().Next(5);

            SetTeamsMatchStatus();
        }        
        public void SetTeamsMatchStatus() 
        {
            if (MatchWasDraw())
            {
                SetDrawMatchStatus();
                return;
            }

            var winner = GetWinner();
            var loser = GetLoser();

            SetLoserMatchStatus(loser);
            SetWinnerMatchStatus(winner);
        }

        public void SetDrawMatchStatus()
        {
            if (!MatchWasDraw()) return;

            Home.TeamStatus.GoalsScored += HomeGoals;
            Home.TeamStatus.TotalPoints += 1;
            Home.TeamStatus.Matchs += 1;

            Opposite.TeamStatus.GoalsScored += HomeGoals;
            Opposite.TeamStatus.TotalPoints += 1;
            Opposite.TeamStatus.Matchs += 1;
        }

        public void SetLoserMatchStatus(Team loser)
        {            
            loser.TeamStatus.GoalsScored += loser == Home ? HomeGoals : OppositeGoals;            
            loser.TeamStatus.Matchs += 1;
        }

        public void SetWinnerMatchStatus(Team winner)
        {
            winner.TeamStatus.GoalsScored += winner == Home ? HomeGoals : OppositeGoals;
            winner.TeamStatus.Matchs += 1;
            winner.TeamStatus.TotalPoints += 3;
            winner.TeamStatus.Victorys += 1;
        }
    }
}
