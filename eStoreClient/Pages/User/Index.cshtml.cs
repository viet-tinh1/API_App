using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace eStoreClient.Pages.User
{
    public class IndexModel : PageModel
    {
        public bool ShowAlert { get; set; } = false;
        public List<Member> listMember { get; set; } = new List<Member>();
        public Member memberSelected { get; set; } = null;
        Member member { get; set; } = new Member();
        private readonly HttpClient client = null;
        private string productApiUrl = "";
        public int? isAdmin = null;
        public IndexModel()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            productApiUrl = "https://localhost:7227/api/member";
        }
        public async Task<IActionResult> OnGet()
        {
            isAdmin = HttpContext.Session.GetInt32("isAdmin");
            if (isAdmin == 1)
            {
                await LoadPage();
                return Page();
            }
            else
            {
                await LoadPage();
                string user = HttpContext.Session.GetString("user");
                if (!string.IsNullOrEmpty(user))
                {
                    return Page();
                }
                else
                {
                    return Redirect("/login");
                }
            }
        }
        public async Task LoadPage()
        {

            isAdmin = HttpContext.Session.GetInt32("isAdmin");
            if (isAdmin != 1)
            {
                string user = HttpContext.Session.GetString("user");

                if (!string.IsNullOrEmpty(user))
                {
                    member = JsonConvert.DeserializeObject<Member>(user);
                }
            }

            if (isAdmin == null)
            {
                HttpResponseMessage respone = await client.GetAsync($"{productApiUrl}/getMemberById?id={member.MemberId}");
                string strData = await respone.Content.ReadAsStringAsync();
                memberSelected = JsonConvert.DeserializeObject<Member>(strData);
            }
            else { 
                HttpResponseMessage respone = await client.GetAsync($"{productApiUrl}/getMember");
                string strData = await respone.Content.ReadAsStringAsync();
                listMember = JsonConvert.DeserializeObject<List<Member>>(strData);

            }
        }
        public async Task<IActionResult> OnPostSelectedMember(int memberId)
        {
            HttpResponseMessage respone = await client.GetAsync($"{productApiUrl}/getMemberById?id={memberId}");
            string strData = await respone.Content.ReadAsStringAsync();
            memberSelected = JsonConvert.DeserializeObject<Member>(strData);
            await LoadPage();
            return Page();
        }
        public async Task<IActionResult> OnPostDeleteMember(int memberId)
        {
            HttpResponseMessage respone = await client.GetAsync($"{productApiUrl}/deleteMember?memberid={memberId}");
            string strData = await respone.Content.ReadAsStringAsync();
            await LoadPage();
            return Page();
        }
        public async Task<IActionResult> OnPostAddNewOrUpdate(int MemberId, string Email, string CompanyName, string City, string Country, string Password)
        {
            if (MemberId == null || Email == null || CompanyName == null || City == null || Country == null || Password == null)
            {
                ShowAlert = true;
                await LoadPage();
                return Page();
            }
            Member member = new Member()
            {
                MemberId = MemberId,
                Email = Email,
                CompanyName = CompanyName,
                City = City,
                Country = Country,
                Password = Password
            };
            if (MemberId == 0)
            {
                HttpResponseMessage respone = await client.PostAsJsonAsync($"{productApiUrl}/createMember", member);
                string strData = await respone.Content.ReadAsStringAsync();
                await LoadPage();
                return Page();
            }
            else
            {
                HttpResponseMessage respone = await client.PostAsJsonAsync($"{productApiUrl}/updateMember", member);
                string strData = await respone.Content.ReadAsStringAsync();
                await LoadPage();
                return Page();

            }


        }
    }
}
