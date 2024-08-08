using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    internal interface IProductRepository
    {
        void deleteProduct(int id);
        void updateProduct(Product p);
        void createProduct(Product p);
        List<Product> getProductsByPriceAndName(string searchName);
        List<Product> getProducts();
        List<ReportSale> getStaticReportSale(DateTime startDate, DateTime endDate);
        Product getProductById(int id);
    }
}
