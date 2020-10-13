using PremierLeague.Application.Models;
using System.Linq;
using Xunit;

namespace PremierLeague.Tests
{
    public class TableTests
    {
        private void AddTeams(Table table)
        {
            table.Teams.Add(new Team("Everton"));
            table.Teams.Add(new Team("Aston Villa"));
            table.Teams.Add(new Team("Leicester City"));
            table.Teams.Add(new Team("Arsenal"));
            table.Teams.Add(new Team("Liverpool"));
            table.Teams.Add(new Team("Tottenham Hotspur"));
            table.Teams.Add(new Team("Chelsea"));
            table.Teams.Add(new Team("Leeds United"));
            table.Teams.Add(new Team("Newcastle United"));
            table.Teams.Add(new Team("West Ham United"));
            table.Teams.Add(new Team("Southampton"));
            table.Teams.Add(new Team("Crystal Palace"));
            table.Teams.Add(new Team("Wolverhampton Wanderers"));
            table.Teams.Add(new Team("Manchester City"));
            table.Teams.Add(new Team("Brighton & Hove Albion"));
            table.Teams.Add(new Team("Manchester United"));
            table.Teams.Add(new Team("West Bromwich Albion"));
            table.Teams.Add(new Team("Burnley"));
            table.Teams.Add(new Team("Sheffield United"));
            table.Teams.Add(new Team("Fulham"));
        }
        [Fact]
        public void Table_GenerateMatchs_MustHave36Rounds()
        {
            var table = new Table();
            AddTeams(table);
            table.GenerateMatchs();

            Assert.Equal(36, table.RoundMatchs.Count);
        }

        [Fact]
        public void Table_GenerateMatchs_MustHave10MatchsEachRound()
        {
            var table = new Table();
            AddTeams(table);
            table.GenerateMatchs();

            foreach (var match in table.RoundMatchs)
            {
                Assert.Equal(10, match.Matchs.Count);
            }
        }

        [Fact]
        public void Table_GenerateMatchs_TeamMustPlayOncePerRound()
        {
            var table = new Table();
            AddTeams(table);
            table.GenerateMatchs();

            foreach (var team in table.Teams)
            {
                foreach (var round in table.RoundMatchs)
                {
                    var result = round.Matchs.Where(r => r.Home.Name == team.Name || r.Opposite.Name == team.Name).Count();
                    Assert.Equal(1, result);
                }
            }
        }

        [Fact]
        public void Table_GenerateMatchs_TeamMustPlayOneGameHomeAndOneOut()
        {
            var table = new Table();
            AddTeams(table);
            table.GenerateMatchs();

            var rounds = table.RoundMatchs;
            var matchs = rounds.SelectMany(r => r.Matchs);

            foreach (var team in table.Teams)
            {
                var countHome = matchs.Where(m => m.Home.Name == team.Name).Count();
                var countOpposite = matchs.Where(m => m.Opposite.Name == team.Name).Count();

                Assert.Equal(18, countHome);
                Assert.Equal(18, countOpposite);
            }
        }
    }
}
