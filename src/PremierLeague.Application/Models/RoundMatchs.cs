using System.Collections.Generic;

namespace PremierLeague.Application.Models
{
    public class RoundMatchs
    {
        public RoundMatchs()
        {            
        }

        public int RoundNumber { get; set; }
        public List<Match> Matchs { get; set; }        
    }
}
