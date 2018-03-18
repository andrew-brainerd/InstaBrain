using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace InstaBrain.Models
{
    public class UsersClient
    {
        private static HttpClient _client;
        private static string accessToken;
        private static string baseUrl = "https://api.instagram.com/";

        #region Boring

        public UsersClient(string access_token)
        {
            InitializeClient();
            accessToken = access_token;
        }

        private static void InitializeClient()
        {
            _client = new HttpClient()
            {
                BaseAddress = new Uri(baseUrl)
            };
        }

        private static void VerifyClient()
        {
            if (_client == null)
            {
                InitializeClient();
            }
        }

        private static string AddAuthToRoute(string route)
        {
            return route + "?access_token=" + accessToken;
        }
        #endregion

        public async Task<string> GetSelf()
        {
            VerifyClient();

            var route = AddAuthToRoute("/v1/users/self/");
            var response = await _client.GetAsync(route);

            var requesUuri = response.RequestMessage.RequestUri.AbsoluteUri;


            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            return responseString;
        }
    }
}
