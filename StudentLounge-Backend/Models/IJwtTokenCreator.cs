namespace StudentLounge_Backend.Models
{
    public interface IJwtTokenCreator
    {
        public string CreateToken(StudentLoungeUser user);
    }
}
