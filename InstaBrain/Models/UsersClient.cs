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

        private static async Task<ApiResponse> AuthorizedGetString(string route, string args = "")
        {
            VerifyClient();

            route = "/v1" + route + "?access_token=" + accessToken + (args != "" ? "&" + args : args);

            var response = await _client.GetAsync(route);
            var requesUuri = response.RequestMessage.RequestUri.AbsoluteUri;
            var responseString = await response.Content.ReadAsStringAsync();

            return new ApiResponse() { Path = route, ResponseString = responseString };
        }
        #endregion

        public async Task<ApiResponse> GetSelf()
        {
            return await AuthorizedGetString("/users/self/");
        }

        public async Task<ApiResponse> GetUserById(string userId)
        {
            return await AuthorizedGetString($"/users/{userId}");
        }

        public async Task<ApiResponse> GetSelfRecentMedia()
        {
            return await AuthorizedGetString("/users/self/media/recent");
        }

        public async Task<ApiResponse> GetUserRecentMedia(string userId, int count = 0)
        {
            return await AuthorizedGetString($"/users/{userId}/media/recent");
        }

        public async Task<ApiResponse> GetSelfLikedMedia()
        {
            return await AuthorizedGetString("/users/self/media/liked");
        }

        public async Task<ApiResponse> GetSelfFollows()
        {
            return await AuthorizedGetString("/users/self/follows");
        }

        public async Task<ApiResponse> GetSelfFollowedBy()
        {
            return await AuthorizedGetString("/users/self/followed-by");
        }
    }
}
