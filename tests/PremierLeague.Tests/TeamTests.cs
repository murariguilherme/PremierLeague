using PremierLeague.Application.Models;
using System;
using Xunit;

namespace PremierLeague.Tests
{
    public class TeamTests
    {        
        private Team _team;

        [Fact]
        public void Team_NewTeam_MustBeValid()
        {
            _team = new Team("Arsenal");
            Assert.True(_team.IsValid());
        }

        [Fact]
        public void Team_NewTeam_MustBeInValid()
        {
            _team = new Team("");
            Assert.False(_team.IsValid());
        }
    }
}
