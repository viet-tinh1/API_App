using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace eStoreClient.Pages.Report
{
    public class IndexModel : PageModel
    {
        public List<ReportSale> listReportSale { get; set; } = new List<ReportSale>();
        private readonly HttpClient client = null;
        private string productApiUrl = "";
        public int? isAdmin = null;
        public IndexModel()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            productApiUrl = "https://localhost:7227/api/product";
        }
        public async Task<IActionResult> OnGet()
        {
            isAdmin = HttpContext.Session.GetInt32("isAdmin");
            if (isAdmin == 1)
            { 
                return Page();
            }
            else
            {             
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
        public async Task<IActionResult> OnPostReportSale(DateTime startdate, DateTime enddate)
        {
            var loginData = new
            {
                StartDate = startdate,
                EndDate = enddate
            };
            HttpResponseMessage respone = await client.PostAsJsonAsync($"{productApiUrl}/getStaticReportSale", loginData);
            string strData = await respone.Content.ReadAsStringAsync();
            listReportSale = JsonConvert.DeserializeObject<List<ReportSale>>(strData);
            return Page();
        }

    }
}
