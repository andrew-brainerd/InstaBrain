using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using InstaBrain.Models;
using System.Net.Http;

namespace InstaBrain.Controllers
{
    public class HomeController : Controller
    {
        public static HttpClient _client;
        private static string baseUrl = "https://api.instagram.com";
        private static string authEndpoint = "/oauth/authorize/";
        private static string clientId = "a70d888fa7114376a251d49fce2f2241";
        private static string redirectUrl = "http://localhost:57300/Home";
        private static string requestUrl = baseUrl + authEndpoint + "?client_id=" + clientId + "&redirect_uri=" + redirectUrl + "&response_type=token";

        public IActionResult Index(string access_token)
        {
            ViewData["AuthorizationEndpoint"] = requestUrl;
            ViewData["AccessToken"] = access_token;

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

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
    }
}
