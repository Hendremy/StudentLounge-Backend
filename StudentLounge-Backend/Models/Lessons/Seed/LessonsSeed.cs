using Microsoft.EntityFrameworkCore;

namespace StudentLounge_Backend.Models.Lessons.Seed
{
    public class LessonsSeed
    {
        public static void Generate(ModelBuilder modelBuilder)
        {
            List<Lesson> lessons = new List<Lesson>
            {
                new Lesson { Id = -1, Name = "Mathématiques"},
                new Lesson { Id = -2, Name = "Informatique"},
                new Lesson { Id = -3, Name = "Anglais"},
                new Lesson { Id = -4, Name = "Cybersécurité"}
            };

            modelBuilder.Entity<Lesson>().HasData(lessons);
        }
    }
}
