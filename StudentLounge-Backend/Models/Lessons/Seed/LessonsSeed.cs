using Microsoft.EntityFrameworkCore;

namespace StudentLounge_Backend.Models.Lessons.Seed
{
    public class LessonsSeed
    {
        public static void Generate(ModelBuilder modelBuilder)
        {
            List<Lesson> lessons = new List<Lesson>
            {
                new Lesson { Id = "70c02712-6f41-11ed-a1eb-0242ac120002", Name = "Mathématiques"},
                new Lesson { Id = "7b4b00ee-6f41-11ed-a1eb-0242ac120002", Name = "Informatique"},
                new Lesson { Id = "7b4b0684-6f41-11ed-a1eb-0242ac120002", Name = "Anglais"},
                new Lesson { Id = "7b4b053a-6f41-11ed-a1eb-0242ac120002", Name = "Cybersécurité"}
            };

            modelBuilder.Entity<Lesson>().HasData(lessons);
        }
    }
}
