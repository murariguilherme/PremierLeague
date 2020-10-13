using PremierLeague.Application.Models;
using System;
using System.Linq;

namespace PremierLeague.Application
{
    class Program
    {
        static void Main(string[] args)
        {
            var table = new Table();
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

            table.GenerateMatchs();

            var allMatchs = table.RoundMatchs.SelectMany(r => r.Matchs).ToList();
            allMatchs.ForEach(m => m.GenerateMatchResult());

            table.GenerateStandings();

            Console.ReadLine();
        }
    }
}
