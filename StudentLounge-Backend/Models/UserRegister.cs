﻿namespace StudentLounge_Backend.Models
{
    public class UserRegister
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string PassHash { get; set; }
        public string PassRepHash { get; set; }

    }
}