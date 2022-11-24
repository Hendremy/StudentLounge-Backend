namespace StudentLounge_Backend.Models.Authentication
{
    public class UserInfo
    {
        public string Id { get; set; }
        public string Token { get; set; }
        public string Fullname { get;set; }
        public string Image { get; set; }

        public UserInfo(string id, string token, string fullname, string image)
        {
            Id = id;
            Token = token;
            Fullname = fullname;
            Image = image;
        }
    }
}
