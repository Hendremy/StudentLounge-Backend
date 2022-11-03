namespace StudentLounge_Backend.Models
{
    public interface ICreateExternalUser
    {
        public string Provider { get; }
        public Task<StudentLoungeUser> Create(string providerKey);
    }
}
