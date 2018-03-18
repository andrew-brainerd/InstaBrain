using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstaBrain.Models
{
    public class ApiResponse
    {
        public String Path { get; set; }
        public String ResponseString { get; set; }
        private static string baseUrl = "https://api.instagram.com";

        public override string ToString()
        {
            return "Path: " + baseUrl + Path + "\n\nResponse: " + ResponseString;
        }
    }
}
