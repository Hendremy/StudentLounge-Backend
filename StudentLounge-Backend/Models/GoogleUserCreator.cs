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
            UserCredential userCredential = await GoogleWebAuthorizationBroker.AuthorizeAsync(_secrets, new string[] { }, providerKey, CancellationToken.None);
            //TODO: Récupérer les infos de l'utilisateur pour lui créer un compte
            //providerKey = l'id user côté google, refaire une requête pour avoir nom prénom & créer le compte puis associer "Google","userid" dans UserLogins ??
            throw new NotImplementedException();
        }
    }
}
