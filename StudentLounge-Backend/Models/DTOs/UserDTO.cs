using Castle.Core.Internal;
using Microsoft.AspNetCore.Identity;
using StudentLounge_Backend.Models.Tutorats;

namespace StudentLounge_Backend.Models.DTOs
{
    public class UserDTO
    {
        public string Id { get; private set; }
        public string Fullname { get; private set; }
        public string? Image { get; private set; }
        public string Username { get; private set; }
        public string Firstname { get; private set; }
        public string Lastname { get; private set; }
        public bool fromGoogle { get; private set; }
        public bool isLockout { get; private set; }

        public UserDTO(AppUser user)
        {
            Id = user.Id;
            Fullname = user.Fullname;
            Image = user.Image;
            Username = user.UserName;
            Firstname = user.Firstname;
            Lastname = user.Lastname;
            fromGoogle = user.PasswordHash.IsNullOrEmpty();
            isLockout = DateTime.Now < user.LockoutEnd;
        }
    }
}
