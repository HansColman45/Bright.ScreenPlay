using Bright.ScreenPlay.Actors;

namespace Bright.ScreenPlay.Abilities
{
    public class CallAnApi : Ability
    {
        private readonly HttpClient _client;

        public HttpClient ApiClient => _client;

        public CallAnApi()
        {
            HttpClientHandler clientHandler = new()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => {
                    //if (development) return true;
                    return sslPolicyErrors == System.Net.Security.SslPolicyErrors.None;
                }
            };
            _client = new HttpClient(clientHandler);
        }
    }
}
