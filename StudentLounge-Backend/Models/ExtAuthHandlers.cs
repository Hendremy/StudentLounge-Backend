using Microsoft.AspNetCore.Identity;

namespace StudentLounge_Backend.Models
{
    public class GoogleAuthHandler : BaseExtAuthHandler
    {
        //TODO: refactoriser dans classe abstraite
        private const string _providerName = "Google";

        private readonly UserManager<StudentLoungeUser> _userManager;

        public GoogleAuthHandler(UserManager<StudentLoungeUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<StudentLoungeUser> HandleAsync(ExtAuthRequest request)  {
            if (CanHandleRequest(request))
            {
                return await CreateGoogleUserAsync(request);
            }
            else
            {
                return await base.HandleAsync(request);
            }
        }

        private async Task<StudentLoungeUser> CreateGoogleUserAsync(ExtAuthRequest request)
        {
            return null;
        }

        private bool CanHandleRequest(ExtAuthRequest request)
        {
            return request.MatchesProvider(_providerName) && request.TokenIsValid;
        }
    }


}
