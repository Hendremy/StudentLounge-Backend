namespace StudentLounge_Backend.Models
{
    public abstract class OAuthAccount
    {
        private int _id;
        private OAuthProvider _oAuthProvider;

        public OAuthAccount(int id, OAuthProvider provider)
        {
            _id = id;
            _oAuthProvider = provider;
        }

        public int Id => _id;
        public OAuthProvider Provider => _oAuthProvider;
    }

    public enum OAuthProvider
    {
        Facebook, Apple, Google
    }
}
