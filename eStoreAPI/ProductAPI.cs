using BusinessObject.Models;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace eStoreAPI
{
    [Route("api/product")]
    [ApiController]
    public class ProductAPI : ControllerBase
    {
        private ProductRepository productRepository = new ProductRepository();
        [HttpGet("getProducts")]
        public ActionResult<IEnumerable<Product>> getProducts()
        {
            return productRepository.getProducts();
        }
        [HttpGet("getProductsByPriceAndName")]
        public ActionResult<IEnumerable<Product>> getProductsByPriceAndName(string NameOrUnitPrice)
        {
            return productRepository.getProductsByPriceAndName(NameOrUnitPrice);
        }

        [HttpPost("getStaticReportSale")]
        public ActionResult<IEnumerable<ReportSale>> getStaticReportSale([FromBody] TimeRange time)
        {
            return productRepository.getStaticReportSale(time.StartDate, time.EndDate);
        }

        [HttpPost("createProduct")]
        public IActionResult createProduct(Product p)
        {
            Product product = new Product()
            {
                CategoryId = p.CategoryId,
                ProductName = p.ProductName,
                Weight = p.Weight,
                UnitPrice = p.UnitPrice,
                UnitsInStock = p.UnitsInStock
            };
            productRepository.createProduct(product);
            return NoContent();
        }

        [HttpPost("updateProduct")]
        public IActionResult updateProduct(Product p)
        {
            Product product = new Product()
            {
                ProductId = p.ProductId,
                CategoryId = p.CategoryId,
                ProductName = p.ProductName,
                Weight = p.Weight,
                UnitPrice = p.UnitPrice,
                UnitsInStock = p.UnitsInStock
            };
            productRepository.updateProduct(product);
            return NoContent();
        }
        [HttpGet("deleteProduct")]
        public IActionResult deleteProduct(int productId)
        {
            productRepository.deleteProduct(productId);
            return NoContent();
        }
        [HttpGet("getProductById")]
        public ActionResult<Product> getProductById(int id)
        {
            return productRepository.getProductById(id);
        }
    }
}
