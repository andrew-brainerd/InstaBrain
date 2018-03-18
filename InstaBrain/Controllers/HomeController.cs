using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using InstaBrain.Models;
using System.Net.Http;
using System.Net.Http.Headers;

namespace InstaBrain.Controllers
{
    public class HomeController : Controller
    {
        public static HttpClient _client;
        private static string baseUrl = "https://api.instagram.com";
        private static string authEndpoint = "/oauth/authorize/";
        private static string tokenEndpoint = "/oauth/access_token/";
        private static string clientId = "a70d888fa7114376a251d49fce2f2241";
        private static string clientSecret = "21858dd90eb6415cb531af9c4b40b23f ";
        private static string redirectUrl = "http://localhost:57300/About";
        private static string requestUrl = baseUrl + authEndpoint + "?client_id=" + clientId + "&redirect_uri=" + redirectUrl + "&response_type=token";

        public IActionResult Index()
        {

            //GetAuthorizedClient();

            return View();
        }

        public async Task<IActionResult> About(string code, string token)
        {
            ViewData["Message"] = "Your application description page.";

            string url = baseUrl + tokenEndpoint + "?client_id=" + clientId + "&client_secret=" + clientSecret + "&grant_type=authorization_code&redirect_uri=" + redirectUrl + "&code=" + code;
            Debug.WriteLine("CODE: " + code);
            //Response.Redirect(url);

            _client = new HttpClient();

            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(url),
                Method = HttpMethod.Get
            };

            HttpResponseMessage response = await _client.SendAsync(request);

            Debug.WriteLine("RESPONSE: " + response);

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public static async void GetAuthorizedClient()
        {
            Debug.WriteLine("Request Url: " + requestUrl);

            _client = new HttpClient();

            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(requestUrl),
                Method = HttpMethod.Get
            };

            HttpResponseMessage response = await _client.SendAsync(request);
            var statusCode = (int)response.StatusCode;
            
            foreach (var header in response.Headers)
            {
                var headerValue = header.Value;
                if (headerValue.GetType().Equals(typeof(String[])))
                {
                    Debug.WriteLine("HEADER IS A STRING ARRAY");
                    foreach (String val in header.Value)
                    {
                        Debug.WriteLine(val);
                    }
                }
                else
                {
                    Debug.WriteLine("HEADER: " + header.Value);
                }
            }

            // We want to handle redirects ourselves so that we can determine the final redirect Location (via header)
            if (statusCode >= 300 && statusCode <= 399)
            {
                var redirectUri = response.Headers.Location;
                if (!redirectUri.IsAbsoluteUri)
                {
                    redirectUri = new Uri(request.RequestUri.GetLeftPart(UriPartial.Authority) + redirectUri);
                    Debug.WriteLine("REDIRECTED: " + redirectUrl);
                }
            }
            else if (!response.IsSuccessStatusCode)
            {
                throw new Exception();
            }
            else
            {
                Debug.WriteLine("SUCCESS NO REDIRECT [" + statusCode + "]");
            }

            var accessToken = "15954374.a70d888.b384d7b869254018ac604bcca2494741";

            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        }
    }
}
