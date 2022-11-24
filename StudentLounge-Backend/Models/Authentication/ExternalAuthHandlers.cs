using Google.Apis.Auth;
using Microsoft.AspNetCore.Identity;

namespace StudentLounge_Backend.Models.Authentication
{
    public class ExternalAuthHandlers : BaseExtAuthHandler
    {
        public ExternalAuthHandlers(IHandleUsers userRepo)
        {
            var googleHandler = new GoogleAuthHandler(userRepo);
            this.Next = googleHandler;
        }
    }

    internal class GoogleAuthHandler : BaseExtAuthHandler
    {
        private const string _providerName = "Google";

        private readonly IHandleUsers _userRepository;

        public GoogleAuthHandler(IHandleUsers userRepository)
        {
            ProviderName = _providerName;
            _userRepository = userRepository;
        }

        public async Task<AppUser> HandleAsync(ExternalAuthRequest request)
        {
            if (CanHandleRequest(request))
            {
                return await AuthenticateUserAsync(request);
            }
            else
            {
                return await base.HandleAsync(request);
            }
        }

        private async Task<AppUser> AuthenticateUserAsync(ExternalAuthRequest request)
        {
            try 
            {
                GoogleJsonWebSignature.Payload payload = await GoogleJsonWebSignature.ValidateAsync(request.Token);
                string userId = payload.Subject;
                var user = await _userRepository.FindExternalUserAsync(request.ProviderName, userId);
                if(user == null)
                {
                    var newUser = CreateUser(payload);
                    var createResult = await _userRepository.CreateExternalUserAsync(request.ProviderName, userId, newUser);
                }
                return user;
            }
            catch (InvalidJwtException ex)
            {
                return null;
            }
        }

        private AppUser CreateUser(GoogleJsonWebSignature.Payload payload)
        {
            return new AppUser()
            {
                Firstname = payload.GivenName,
                Lastname = payload.FamilyName,
                Email = payload.Email,
                Image = payload.Picture
            };
        }
        
    }
}
