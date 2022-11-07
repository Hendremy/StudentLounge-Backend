namespace StudentLounge_Backend.Models
{
    public interface IHandleExtAuth
    {
        IHandleExtAuth Next { get;  set; }
        Task<StudentLoungeUser> HandleAsync(ExtAuthRequest request);
    }

    public class ExtAuthRequest
    {
        public string ProviderName { get; set; }
        public string Token { get; set; }

        public bool MatchesProvider(string providerName) => ProviderName == providerName;
        public bool TokenIsValid => !string.IsNullOrEmpty(Token);
    }

    public abstract class BaseExtAuthHandler : IHandleExtAuth
    {
        public IHandleExtAuth Next { get; set; }

        public async Task<StudentLoungeUser> HandleAsync(ExtAuthRequest request)
        {
            StudentLoungeUser? user = null;
            if (Next != null)
            {
                user = await Next.HandleAsync(request);
            }
            return user;
        }
    }
}
