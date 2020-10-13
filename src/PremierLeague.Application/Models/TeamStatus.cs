using System;
using System.Collections.Generic;
using System.Text;

namespace PremierLeague.Application.Models
{
    public class TeamStatus
    {        
        public int Matchs { get; set; }
        public int TotalPoints { get; set; }
        public int GoalsScored { get; set; }
        public int Victorys { get; set; }

        public TeamStatus()
        {
            Matchs = 0;
            TotalPoints = 0;
            GoalsScored = 0;
            Victorys = 0;
        }

        public bool IsValid()
        {
            if (Matchs < 0 || TotalPoints < 0 || GoalsScored < 0 || Victorys < 0) return false;

            return true;
        }
    }
}
