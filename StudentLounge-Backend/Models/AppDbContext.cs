﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudentLounge_Backend.Models.Files;
using StudentLounge_Backend.Models.Tutorats;
using StudentLounge_Backend.Models.Lessons.Seed;
using StudentLounge_Backend.Models.Agendas;
using StudentLounge_Backend.Models.Appointments;

namespace StudentLounge_Backend.Models
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

        public DbSet<Tutoring> Tutorings { get; set; }

        public DbSet<Lesson> Lessons { get; set; }

        public DbSet<LessonFile> LessonFiles { get; set; }

        public DbSet<AppUser> AppUsers { get; set; }

        public DbSet<Appointment> Appointments { get; set; }

        public DbSet<Agenda> Agendas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            LessonsSeed.Generate(modelBuilder);

            modelBuilder.Entity<Tutoring>().HasOne(t => t.Tutor)
                .WithMany(u => u.AcceptedTutorings)
                .HasForeignKey(t => t.TutorId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Tutoring>().HasOne(t => t.Tutored)
                .WithMany(u => u.TutoringRequests)
                .HasForeignKey(t => t.TutoredId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Agenda>().HasMany(a => a.AgendaEvents)
                .WithOne(ae => ae.Agenda)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Agenda>().HasOne(a => a.User)
                .WithMany(u => u.Agendas)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
