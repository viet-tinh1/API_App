using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;


namespace eStoreClient.Pages.Home
{
    public class IndexModel : PageModel
    {
        public bool ShowAlert { get; set; } = false;
        public List<Product> listProduct { get; set; } = new List<Product>();
        public Product productSelected { get; set; } = null;
        public int? isAdmin = null;
        Member member { get; set; } = new Member();
        private readonly HttpClient client = null;
        private string productApiUrl = "";
        private string orderDetailApiUrl = "";
        private string CategoryApiUrl = "";
        public IndexModel()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            productApiUrl = "https://localhost:7227/api/product";
            orderDetailApiUrl = "https://localhost:7227/api/orderdetail";
            CategoryApiUrl = "https://localhost:7227/api/CategoryAPI";
        }
        public async Task<IActionResult> OnGet()
        {
            ViewData["CategoryId"] = new SelectList(await GetCategories(), "CategoryId", "CategoryName");

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
        private async Task<List<Category>> GetCategories()
        {

            HttpResponseMessage responseMessage = await client.GetAsync(CategoryApiUrl);
            string strData = await responseMessage.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            var resultList = System.Text.Json.JsonSerializer.Deserialize<List<Category>>(strData, options);
            return resultList ?? new List<Category>();
        }
        public async Task LoadPage()
        {
            isAdmin = HttpContext.Session.GetInt32("isAdmin");
            if (isAdmin == 1)
            {
                HttpResponseMessage respone = await client.GetAsync($"{productApiUrl}/getProducts");
                string strData = await respone.Content.ReadAsStringAsync();
                listProduct = JsonConvert.DeserializeObject<List<Product>>(strData);
            }
            else
            {
                string user = HttpContext.Session.GetString("user");
                if (!string.IsNullOrEmpty(user))
                {
                    member = JsonConvert.DeserializeObject<Member>(user);
                }
                HttpResponseMessage respone = await client.GetAsync($"{productApiUrl}/getProducts");
                string strData = await respone.Content.ReadAsStringAsync();
                listProduct = JsonConvert.DeserializeObject<List<Product>>(strData);


            }


        }
        public async Task<IActionResult> OnPostSelectedProduct(int productId)
        {
            HttpResponseMessage respone = await client.GetAsync($"{productApiUrl}/getProductById?id={productId}");
            string strData = await respone.Content.ReadAsStringAsync();
            productSelected = JsonConvert.DeserializeObject<Product>(strData);
            await LoadPage();
            return Page();
        }
        public async Task<IActionResult> OnPostByPriceAndName(string search)
        {
            if (string.IsNullOrEmpty(search))
            {
                await LoadPage();
                return Page();
            }
            else
            {
                HttpResponseMessage respone = await client.GetAsync($"{productApiUrl}/getProductsByPriceAndName?NameOrUnitPrice={search}");
                string strData = await respone.Content.ReadAsStringAsync();
                listProduct = JsonConvert.DeserializeObject<List<Product>>(strData);
                isAdmin = HttpContext.Session.GetInt32("isAdmin");
                if (isAdmin != 1) {
                    string user = HttpContext.Session.GetString("user");
                    if (!string.IsNullOrEmpty(user))
                    {
                        member = JsonConvert.DeserializeObject<Member>(user);
                    }
                }
              
            }
                return Page();
            }

        
        public async Task<IActionResult> OnPostDeleteProduct(int productId)
        {
            HttpResponseMessage respone = await client.GetAsync($"{productApiUrl}/deleteProduct?productId={productId}");
            string strData = await respone.Content.ReadAsStringAsync();
            await LoadPage();
            return Page();
        }
        public async Task<IActionResult> OnPostAddOrder(int quantity, int productId)
        {
            string user = HttpContext.Session.GetString("user");
            if (!string.IsNullOrEmpty(user))
            {
                member = JsonConvert.DeserializeObject<Member>(user);
            }
            var loginData = new
            {
                Quantity = quantity,
                ProductId = productId,
                MemberIds = member.MemberId
            };

            HttpResponseMessage respone = await client.PostAsJsonAsync($"{orderDetailApiUrl}/addOrder", loginData);
            string strData = await respone.Content.ReadAsStringAsync();
            await LoadPage();
            return Page();
        }
        public async Task<IActionResult> OnPostAddNewOrUpdate(int ProductId, int CategoryId, string ProductName, int Weight, int UnitPrice, int UnitsInStock, string myCheckbox)
        {
           
            if (ProductId == null || CategoryId == null || ProductName == null || Weight == null || string.IsNullOrEmpty(ProductName) || UnitPrice == null || UnitsInStock == null)
            {
                ShowAlert = true;
                await LoadPage();
                return Page();
            }
            Product product = new Product()
            {
                ProductId = ProductId,
                CategoryId = CategoryId,
                ProductName = ProductName,
                Weight = Weight,
                UnitPrice = UnitPrice,
                UnitsInStock = UnitsInStock
            };
            if (myCheckbox != null)
            {
                HttpResponseMessage respone = await client.PostAsJsonAsync($"{productApiUrl}/createProduct", product);
                string strData = await respone.Content.ReadAsStringAsync();
                await LoadPage();
                return Page();
            }
            else
            {
                HttpResponseMessage respone = await client.PostAsJsonAsync($"{productApiUrl}/updateProduct", product);
                string strData = await respone.Content.ReadAsStringAsync();
                await LoadPage();
                return Page();

            }
        }
    }
}
