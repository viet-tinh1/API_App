using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace eStoreClient.Pages.OrderHistory
{
    public class IndexModel : PageModel
    {
        private readonly HttpClient client = null;
        private string MemberApiUrl = "";
        Member member { get; set; } = new Member();
        public int? isAdmin = null;
        public Order orderSelected { get; set; } = null;
        public List<Order> listOrder { get; set; } = new List<Order>();
        public IndexModel()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            MemberApiUrl = "https://localhost:7227/api/orderdetail";
        }
        public async Task<IActionResult> OnGet()
        {
            isAdmin = HttpContext.Session.GetInt32("isAdmin");
            if (isAdmin == 1)
            {
                HttpResponseMessage respone = await client.GetAsync($"{MemberApiUrl}/GetOrderDetail");
                string strData = await respone.Content.ReadAsStringAsync();
                listOrder = JsonConvert.DeserializeObject<List<Order>>(strData);
                return Page();
            }
            else
            {
                string user = HttpContext.Session.GetString("user");
                if (!string.IsNullOrEmpty(user))
                {
                    member = JsonConvert.DeserializeObject<Member>(user);
                    HttpResponseMessage respone = await client.GetAsync($"{MemberApiUrl}/GetOrderDetailsByMemberId?id={member.MemberId}");
                    string strData = await respone.Content.ReadAsStringAsync();
                    listOrder = JsonConvert.DeserializeObject<List<Order>>(strData);
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
            if (isAdmin == 1)
            {
                HttpResponseMessage respone = await client.GetAsync($"{MemberApiUrl}/GetOrderDetail");
                string strData = await respone.Content.ReadAsStringAsync();
                listOrder = JsonConvert.DeserializeObject<List<Order>>(strData);
            }
            else
            {
                string user = HttpContext.Session.GetString("user");
                if (!string.IsNullOrEmpty(user))
                {
                    member = JsonConvert.DeserializeObject<Member>(user);
                    HttpResponseMessage respone = await client.GetAsync($"{MemberApiUrl}/GetOrderDetailsByMemberId?id={member.MemberId}");
                    string strData = await respone.Content.ReadAsStringAsync();
                    listOrder = JsonConvert.DeserializeObject<List<Order>>(strData);
                }
            }


        }
        public async Task<IActionResult> OnPostSelectedOrder(int orderId)
        {
            HttpResponseMessage respone = await client.GetAsync($"{MemberApiUrl}/getOrderById?id={orderId}");
            string strData = await respone.Content.ReadAsStringAsync();
            orderSelected = JsonConvert.DeserializeObject<Order>(strData);
            await LoadPage();
            return Page();
        }
        public async Task<IActionResult> OnPostDeleteOrder(int orderId)
        {
            HttpResponseMessage respone = await client.GetAsync($"{MemberApiUrl}/DeleteOrderDetail?id={orderId}");
            string strData = await respone.Content.ReadAsStringAsync();
            orderSelected = JsonConvert.DeserializeObject<Order>(strData);
            await LoadPage();
            return Page();
        }
        public async Task<IActionResult> OnPostUpdateOrder(int OrderId, DateTime RequiredDate, DateTime OrderDate, DateTime ShippedDate)
        {
            Order o = new Order() {
                OrderId = OrderId,
                RequiredDate = RequiredDate,
                OrderDate = OrderDate,
                ShippedDate = ShippedDate
            };
            HttpResponseMessage respone = await client.PostAsJsonAsync($"{MemberApiUrl}/UpdateOrderDetail",o);
            await LoadPage();
            return Page();
        }
    }
}
