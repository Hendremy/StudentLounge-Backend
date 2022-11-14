using System.ComponentModel.DataAnnotations;

namespace StudentLounge_Backend.Models
{
    public class Lesson
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<AppUser> Users { get; set; } = new List<AppUser>();
    }
}
