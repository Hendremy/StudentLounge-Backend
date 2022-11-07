namespace StudentLounge_Backend.Models.Authentication
{

    public class ExternalAuthRequest
    {
        public string ProviderName { get; set; }
        public string Token { get; set; }

        public bool MatchesProvider(string providerName) => providerName != null && ProviderName == providerName;
        public bool IsValid => !string.IsNullOrEmpty(ProviderName) && !string.IsNullOrEmpty(Token);
    }
}
