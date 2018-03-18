using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using InstaBrain.Models;
using System.Net.Http;

namespace InstaBrain.Controllers
{
    public class HomeController : Controller
    {
        private static string baseUrl = "https://api.instagram.com";
        private static string authEndpoint = "/oauth/authorize/";
        private static string clientId = "a70d888fa7114376a251d49fce2f2241";
        private static string redirectUrl = "http://localhost:57300/Home/Authorize";
        private static string requestUrl = baseUrl + authEndpoint + "?client_id=" + clientId + "&redirect_uri=" + redirectUrl + "&response_type=token";

        public IActionResult Index()
        {
            var sessionToken = HttpContext.Session.GetString("AccessToken");

            if (sessionToken == null)
            {
                Response.Redirect("/Home/Authorize");
            }

            return View();
        }

        public IActionResult Authorize(string access_token)
        {
            var doneTriedToAuthorizeBefore = HttpContext.Session.GetString("TriedToAuthBefore");

            if (doneTriedToAuthorizeBefore == null)
            {
                HttpContext.Session.SetString("TriedToAuthBefore", "totes");
                Response.Redirect(requestUrl);
            }

            if (access_token != null)
            {
                HttpContext.Session.SetString("AccessToken", access_token);
                Response.Redirect("/Home");
            }

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
