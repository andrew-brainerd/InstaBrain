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

        #region Setup and Tools

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

        private static async Task<string> AuthorizedGetString(string route)
        {
            VerifyClient();

            route = AddAuthToRoute("/v1" + route);
            var response = await _client.GetAsync(route);
            var requesUuri = response.RequestMessage.RequestUri.AbsoluteUri;
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            return responseString;
        }
        #endregion

        public async Task<string> GetSelf()
        {
            return await AuthorizedGetString("/users/self/");
        }

        public async Task<string> GetUserById(string userId)
        {
            return await AuthorizedGetString("/users/" + userId);
        }

        public async Task<string> GetSelfRecentMedia()
        {
            return await AuthorizedGetString("/users/self/media/recent");
        }
    }
}
