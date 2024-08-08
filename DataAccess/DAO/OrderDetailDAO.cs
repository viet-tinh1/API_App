using BusinessObject.Models;

namespace DataAccess.DAO
{
    public class OrderDetailDAO
    {
        readonly PRN231_Assignment1Context _context = new PRN231_Assignment1Context();

        public OrderDetailDAO()
        {
        }
        public void addNewOrder(int productId, int quantity, int memberId)
        {
            DateTime currentDateTime = DateTime.Now;
            Product product = _context.Products.FirstOrDefault(x => x.ProductId == productId);
            Order newOrder = new Order
            {
                MemberId = memberId,
                Freight = "",
                OrderDate = currentDateTime,
                ShippedDate = currentDateTime,
                RequiredDate = currentDateTime
            };


            OrderDetail orderDetail = new OrderDetail
            {
                ProductId = productId,
                UnitPrice = product.UnitPrice,
                Quantity = quantity,
                Discount = 0,
                Order = newOrder
            };


            _context.OrderDetails.Add(orderDetail);
            _context.SaveChanges();
        }
        public List<Order> GetOrderDetail()
        {
            var a = _context.Orders.ToList();
            return a;
        }
        public List<Order> GetOrderDetailsByMemberId(int id)
        {
            return _context.Orders.Where(x => x.MemberId == id).ToList();
        }
        public void UpdateOrderDetail(Order o)
        {
            Order od = _context.Orders.FirstOrDefault(x => x.OrderId == o.OrderId);
            od.ShippedDate = o.ShippedDate;
            od.RequiredDate = o.RequiredDate;
            od.OrderDate = o.OrderDate;
            _context.Orders.Update(od);
            _context.SaveChanges();
        }
        public void DeleteOrderDetail(int id)
        {
            Order a = _context.Orders.FirstOrDefault(x => x.OrderId == id);
            OrderDetail ad = _context.OrderDetails.FirstOrDefault(x => x.OrderId == id);
            _context.OrderDetails.Remove(ad);
            _context.Orders.Remove(a);
            _context.SaveChanges();
        }
        public Order getOrderById(int id)
        {
            return _context.Orders.FirstOrDefault(x => x.OrderId == id);
        }
    }
}
