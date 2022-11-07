using Google.Apis.Auth.OAuth2;

namespace StudentLounge_Backend.Models
{
    public class GoogleUserCreator : ICreateExternalUser
    {
        private ClientSecrets _secrets;
        public string Provider => "Google";

        public GoogleUserCreator(ClientSecrets secrets) 
        {
            _secrets = secrets;
        }

        public async Task<StudentLoungeUser> Create(string providerKey)
        {
            return null;
        }
    }
}
