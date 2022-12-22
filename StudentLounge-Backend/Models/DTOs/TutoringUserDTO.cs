namespace StudentLounge_Backend.Models.DTOs
{
    public class TutoringUserDTO
    {
        public string Name { get; set; }
        public string? Image { get; set; }

        public TutoringUserDTO(string name, string? image)
        {
            Name = name;
            Image = image;
        }
    }
}
