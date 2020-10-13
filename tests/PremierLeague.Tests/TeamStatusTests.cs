using PremierLeague.Application.Models;
using Xunit;

namespace PremierLeague.Tests
{
    public class TeamStatusTests
    {
        private Team _teamValid = new Team("Arsenal");
        private Team _teamInvalid = new Team("");        

        [Fact]
        public void Team_NewTeam_MustBeValid()
        {            
            Assert.True(_teamValid.IsValid());
        }

        [Fact]
        public void Team_NewTeam_MustBeInValid()
        {            
            Assert.False(_teamInvalid.IsValid());

            _teamInvalid = new Team("Arsenal") { TeamStatus = new TeamStatus() { GoalsScored = -1 } };
            Assert.False(_teamInvalid.IsValid());

            _teamInvalid = new Team("Arsenal") { TeamStatus = new TeamStatus() { Matchs = -1 } };
            Assert.False(_teamInvalid.IsValid());

            _teamInvalid = new Team("Arsenal") { TeamStatus = new TeamStatus() { TotalPoints = -1 } };
            Assert.False(_teamInvalid.IsValid());

            _teamInvalid = new Team("Arsenal") { TeamStatus = new TeamStatus() { Victorys = -1 } };
            Assert.False(_teamInvalid.IsValid());
        }
    }
}
