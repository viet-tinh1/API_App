using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class ProductDAO
    {
        readonly PRN231_Assignment1Context _context = new PRN231_Assignment1Context();

        public ProductDAO()
        {
        }

        public ProductDAO(PRN231_Assignment1Context context) {
            _context = context;
        }
        public List<Product> getProducts()
        {
         return _context.Products.ToList();
        }
        public List<Product> getProductsByPriceAndName(string NameOrUnitPrice)
        {
            if (int.TryParse(NameOrUnitPrice, out int number))
            {
                return _context.Products.Where(x => x.UnitPrice == Int32.Parse(NameOrUnitPrice)).ToList();
            }
            else {
                return _context.Products.Where(x => x.ProductName.Contains(NameOrUnitPrice)).ToList();

            }
        }
        public List<ReportSale> getStaticReportSale(DateTime startDate, DateTime endDate)
        {
            List<ReportSale> result = _context.OrderDetails
                  .Include(x => x.Order)
                  .Include(x => x.Product)
                  .Where(x => x.Order.OrderDate >= startDate && x.Order.OrderDate <= endDate)
                  .GroupBy(x => x.Product.ProductName)
                  .Select(group => new ReportSale
                  {
                      ProductName = group.Key,
                      Quantity = group.Sum(x => x.Quantity),
                  })
                  .OrderByDescending(x => x.Quantity)
                  .ToList();

            return result;
        }
        public void deleteProduct(int id)
        {
            var od = _context.OrderDetails.Where(x => x.ProductId == id);
            _context.OrderDetails.RemoveRange(od);
            var p = _context.Products.FirstOrDefault(x => x.ProductId == id);
            _context.Products.Remove(p);
            _context.SaveChanges();
        }
        public void createProduct(Product p)
        {
          _context.Products.Add(p);
           _context.SaveChanges();

        }

        public void updateProduct(Product p)
        {
            Product product = _context.Products.FirstOrDefault(x => x.ProductId == p.ProductId);
            product.ProductName = p.ProductName;
            product.UnitPrice = p.UnitPrice;
            product.UnitsInStock = p.UnitsInStock;
            product.CategoryId = p.CategoryId;
            product.Weight = p.Weight;
            _context.Products.Update(product);
            _context.SaveChanges();
        }
        public Product getProductById(int id)
        {
         return _context.Products.FirstOrDefault(x => x.ProductId == id);

        }
    }
}
