namespace StudentLounge_Backend.Models.Authentication
{
    public interface ICreateToken
    {
        public string Create(AppUser user);
    }
}
