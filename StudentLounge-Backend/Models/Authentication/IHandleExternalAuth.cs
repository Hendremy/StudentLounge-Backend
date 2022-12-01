namespace StudentLounge_Backend.Models.Authentication
{
    public interface IHandleExternalAuth
    {
        IHandleExternalAuth Next { get;  set; }
        Task<AppUser> HandleAsync(ExternalAuthRequest request);
    }

    public abstract class BaseExtAuthHandler : IHandleExternalAuth
    {
        protected string ProviderName { get; init; }
        public IHandleExternalAuth Next { get; set; }

        public async virtual Task<AppUser> HandleAsync(ExternalAuthRequest request)
        {
            AppUser user = null;
            if (Next != null)
            {
                user = await Next.HandleAsync(request);
            }
            return user;
        }

        protected bool CanHandleRequest(ExternalAuthRequest request)
        {
            return request.IsValid && request.MatchesProvider(ProviderName);
        }
    }
}
