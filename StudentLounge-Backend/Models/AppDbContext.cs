﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudentLounge_Backend.Models.Files;
using StudentLounge_Backend.Models.Lessons.Seed;

namespace StudentLounge_Backend.Models
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

        public DbSet<Lesson> Lessons { get; set; }

        public DbSet<LessonFile> LessonFiles { get; set; }

        public DbSet<AppUser> AppUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            LessonsSeed.Generate(modelBuilder);
        }
    }
}
