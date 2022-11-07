using Google.Apis.Auth;
using Microsoft.AspNetCore.Identity;

namespace StudentLounge_Backend.Models
{
    public class GoogleAuthHandler : BaseExtAuthHandler
    {
        private const string _providerName = "Google";

        private readonly IHandleUsers _userRepository;

        public GoogleAuthHandler(IHandleUsers userRepository)
        {
            ProviderName = _providerName;
            _userRepository = userRepository;
        }

        public async Task<AppUser> HandleAsync(ExtAuthRequest request)
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

        private async Task<AppUser> AuthenticateUserAsync(ExtAuthRequest request)
        {
            try 
            {
                GoogleJsonWebSignature.Payload payload = await GoogleJsonWebSignature.ValidateAsync(request.Token);
                var userId = payload.Subject;
                var user = await _userRepository.FindExternalUserAsync(request.ProviderName, userId);
                if(user == null)
                {
                    user = CreateUser(payload);
                    user = await _userRepository.CreateExternalUserAsync(request.ProviderName, userId, user);
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
                FirstName = payload.GivenName,
                LastName = payload.FamilyName,
                Email = payload.Email,
                Image = payload.Picture
            };
        }
        
    }
}
