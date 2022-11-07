namespace StudentLounge_Backend.Models
{
    public interface IHandleExtAuth
    {
        IHandleExtAuth Next { get;  set; }
        Task<AppUser> HandleAsync(ExtAuthRequest request);
    }

    public abstract class BaseExtAuthHandler : IHandleExtAuth
    {
        protected string ProviderName { get; init; }
        public IHandleExtAuth Next { get; set; }

        public async Task<AppUser> HandleAsync(ExtAuthRequest request)
        {
            AppUser? user = null;
            if (Next != null)
            {
                user = await Next.HandleAsync(request);
            }
            return user;
        }

        protected bool CanHandleRequest(ExtAuthRequest request)
        {
            return request.IsValid && request.MatchesProvider(ProviderName);
        }
    }

    public class ExtAuthRequest
    {
        public string ProviderName { get; set; }
        public string Token { get; set; }

        public bool MatchesProvider(string providerName) => providerName != null && ProviderName == providerName;
        public bool IsValid => !string.IsNullOrEmpty(ProviderName) && !string.IsNullOrEmpty(Token);
    }

}
