namespace StudentLounge_Backend.Models.Authentication
{
    public class UserInfoCreator : ICreateUserInfo
    {
        public UserInfo Create(ICreateToken tokenCreator, AppUser user)
        {
            string token = tokenCreator.Create(user);
            return new UserInfo(user.Id, token, user.Fullname, user.Image);
        }
    }
}
