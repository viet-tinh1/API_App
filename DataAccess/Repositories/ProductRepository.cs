using BusinessObject;
using BusinessObject.Models;
using DataAccess.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private ProductDAO productDAO = new ProductDAO();

        public void deleteProduct(int id)
        {
         productDAO.deleteProduct(id);
        }

        public List<Product> getProducts()
        {
            return productDAO.getProducts();
          
        }

        public List<Product> getProductsByPriceAndName(string searchName)
        {
            return productDAO.getProductsByPriceAndName(searchName);
        }

        public List<ReportSale> getStaticReportSale(DateTime startDate, DateTime endDate)
        {
            return productDAO.getStaticReportSale(startDate, endDate);
        }

        public void createProduct(Product p)
        {
            productDAO.createProduct(p);
        }

        public void updateProduct(Product p)
        {
           productDAO.updateProduct(p);
        }

        public Product getProductById(int id)
        {
           return productDAO.getProductById(id);
        }
    }
}
