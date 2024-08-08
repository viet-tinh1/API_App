using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Diagnostics.Metrics;
using System.Net.Http.Headers;

namespace eStoreClient.Pages.Login
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguration _configuration;
        public bool ShowAlert { get; set; } = false;

        public string DefaultEmail { get; private set; }
        public string DefaultPassword { get; private set; }

        private readonly HttpClient client = null;
        private string MemberApiUrl = "";
        public IndexModel(IConfiguration configuration)
        {
            _configuration = configuration;
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            MemberApiUrl = "https://localhost:7227/api/member";
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost(string Username, string Password)
        {
            if (Username.Equals(_configuration["DefaultAccount:Email"]) && Password.Equals(_configuration["DefaultAccount:Password"]))
            {
                HttpContext.Session.SetInt32("isAdmin", 1);
                HttpContext.Session.SetString("user", "Admin");
                return Redirect("/home");
            }
            else
            {

                var loginData = new
                {
                    email = Username,
                    password = Password
                };
                HttpResponseMessage respone = await client.PostAsJsonAsync($"{MemberApiUrl}/checkLogin", loginData);
                string strData = await respone.Content.ReadAsStringAsync();
                Member member = JsonConvert.DeserializeObject<Member>(strData);
                if (member == null)
                {
                    ShowAlert = true;
                    return Page();
                }
                else
                {
                    HttpContext.Session.SetString("user", System.Text.Json.JsonSerializer.Serialize(member));
                    return Redirect("/home");
                }
            }
        }
    }
}
