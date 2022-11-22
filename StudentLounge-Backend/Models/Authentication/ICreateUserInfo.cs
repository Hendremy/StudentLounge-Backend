namespace StudentLounge_Backend.Models.Authentication
{
    public interface ICreateUserInfo
    {
        UserInfo Create(ICreateToken tokenCreator, AppUser user);
    }
}
