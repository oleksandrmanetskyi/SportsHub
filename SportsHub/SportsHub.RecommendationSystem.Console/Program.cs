using CommandLine;
using SportsHub.RecommendationSystem.ConsoleParser;
using System;
using System.Collections.Generic;

namespace SportsHub.RecommendationSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<RecommendCommand, LikeCommand, TestCommand>(args)
                .WithParsed<ICommand>(t => t.Execute());
        }
    }
}
