using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using SportsHub.DataAccess.Entities;

namespace SportsHub.DataAccess
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            var kindNames = new string[]
                {"Archery","Athletics","Billiards","Bobsleigh","Bowling","Boxing", "Canoe", "Chess","Climbing", "Cycling",
                    "Dancing", "Darts", "Discus", "Fencing", "Figure", "Skating", "Fishing","Gliding", "Gymnastics", "Hammer Throwing",
                    "Hang-gliding", "Diving", "High jump", "Hiking", "Horse", "Racing", "Hunting", "Ice Hockey", "Pentathlon", "Motorsports",
                    "Mountaineering", "Paintball", "Parachuting", "Race", "Gymnastics", "Riding", "Skipping", "Rowing", "Running", "Sailing",
                    "Shooting", "Shot put", "Skateboarding", "Skating", "Jumping", "Skiing", "Snowboarding", "Football", "Surfing", "Swimming",
                    "Tennis", "Walking", "Water Polo", "Waterski", "Lifting", "Windsurfing", "Wrestling"
                }
            ;
            var kinds = kindNames.Select((x, y) => new SportKind() {Id = y + 1, Name = x}).ToList();
            modelBuilder.Entity<SportKind>()
                .HasData(kinds);
            //modelBuilder.Entity<News>().HasData(
            //new News{Id = 1,Description = });
        }
    }
}
