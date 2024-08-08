using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Repositories;
using Repositories.Implements;

namespace eStoreAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryAPI : ControllerBase
    {
        private ICategoryRepository repository = new CategoryRepository();

        [HttpGet]
        public ActionResult<IEnumerable<Category>> GetProducts() => repository.GetCategory();
    }
}
