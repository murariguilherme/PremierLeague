using PremierLeague.Application.Models;
using Xunit;

namespace PremierLeague.Tests
{
    public class MatchTests
    {
        private Team _teamValid1 = new Team("Arsenal");
        private Team _teamValid2 = new Team("Chelsea");
        private Team _teamInvalid1 = new Team("");

        [Fact]
        public void Match_NewMatch_MustBeValid()
        {
            var match = new Match(_teamValid1, _teamValid2);

            for (var i = 0; i <= 300; i++)
            {
                match.GenerateMatchResult();
                Assert.True(match.IsValid());
            }            
        }

        [Fact]
        public void Match_NewMatch_MustBeInvalid()
        {
            var match = new Match(_teamValid1, _teamInvalid1);
            Assert.False(match.IsValid());

            match = new Match(_teamInvalid1, _teamValid2);
            Assert.False(match.IsValid());
        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(0, 0)]
        [InlineData(2, 2)]       
        public void Match_SetMatchGoals_MustBeDraw(int homeGoals, int oppositeGoals)
        {
            var match = new Match(_teamValid1, _teamValid2);
            match.HomeGoals = homeGoals;
            match.OppositeGoals = oppositeGoals;

            Assert.True(match.MatchWasDraw());
        }

        [Theory]
        [InlineData(2, 1)]
        [InlineData(0, 1)]
        [InlineData(2, 3)]
        public void Match_SetMatchGoals_MustNotBeDraw(int homeGoals, int oppositeGoals)
        {
            var match = new Match(_teamValid1, _teamValid2);
            match.HomeGoals = homeGoals;
            match.OppositeGoals = oppositeGoals;

            Assert.False(match.MatchWasDraw());
        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(0, 0)]
        [InlineData(2, 2)]
        public void Match_MatchIsDraw_MustIncrementOnePointAndOneMatch(int homeGoals, int oppositeGoals)
        {
            var match = new Match(_teamValid1, _teamValid2);
            match.HomeGoals = homeGoals;
            match.OppositeGoals = oppositeGoals;

            match.SetTeamsMatchStatus();

            Assert.Equal(1, match.Opposite.TeamStatus.TotalPoints);
            Assert.Equal(1, match.Home.TeamStatus.TotalPoints);
            Assert.Equal(homeGoals, match.Home.TeamStatus.GoalsScored);
            Assert.Equal(oppositeGoals, match.Opposite.TeamStatus.GoalsScored);
            Assert.Equal(1, match.Home.TeamStatus.Matchs);
            Assert.Equal(1, match.Opposite.TeamStatus.Matchs);
        }

        [Theory]
        [InlineData(3, 1)]
        [InlineData(2, 0)]
        [InlineData(4, 2)]
        public void Match_MatchIsNotDraw_MustIncrementToLoserOnlyMatchAndGoals(int homeGoals, int oppositeGoals)
        {
            var match = new Match(_teamValid1, _teamValid2);
            match.HomeGoals = homeGoals;
            match.OppositeGoals = oppositeGoals;

            match.SetTeamsMatchStatus();

            Assert.Equal(oppositeGoals, _teamValid2.TeamStatus.GoalsScored);
            Assert.Equal(1, _teamValid2.TeamStatus.Matchs);
            Assert.Equal(0, _teamValid2.TeamStatus.Victorys);
            Assert.Equal(0, _teamValid2.TeamStatus.TotalPoints);
        }

        [Theory]
        [InlineData(3, 1)]
        [InlineData(2, 0)]
        [InlineData(4, 2)]
        public void Match_MatchIsNotDraw_MustIncrementToWinnerAllStatus(int homeGoals, int oppositeGoals)
        {
            var match = new Match(_teamValid1, _teamValid2);
            match.HomeGoals = homeGoals;
            match.OppositeGoals = oppositeGoals;

            match.SetTeamsMatchStatus();

            Assert.Equal(homeGoals, _teamValid1.TeamStatus.GoalsScored);
            Assert.Equal(1, _teamValid1.TeamStatus.Matchs);
            Assert.Equal(1, _teamValid1.TeamStatus.Victorys);
            Assert.Equal(3, _teamValid1.TeamStatus.TotalPoints);
        }
    }
}