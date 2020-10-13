using System;
using System.Text;

namespace PremierLeague.Application.Models
{
    public class Team
    {
        public string Name { get; set; }
        public TeamStatus TeamStatus { get; set; }
        public Team(string name)
        {
            Name = name;
            TeamStatus = new TeamStatus();
        }

        public bool IsValid()
        {
            return !string.IsNullOrEmpty(Name) && TeamStatus.IsValid();            
        }
    }
}
