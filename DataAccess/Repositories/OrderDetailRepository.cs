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
    public class OrderDetailRepository : IOrderDetailRepository
    {
        private OrderDetailDAO orderDetailDAO = new OrderDetailDAO();
        public void addNewOrder(int productId, int quantity, int memberId)
        {
            orderDetailDAO.addNewOrder(productId, quantity, memberId);
            
        }

        public void DeleteOrderDetail(int id)
        {
           orderDetailDAO.DeleteOrderDetail(id);
        }

        public Order getOrderById(int id)
        {
         return orderDetailDAO.getOrderById(id);
        }

        public List<Order> GetOrderDetail()
        {
            return orderDetailDAO.GetOrderDetail();
        }
        public List<Order> GetOrderDetailsByMemberId(int id) {
            return orderDetailDAO.GetOrderDetailsByMemberId(id);
        }

        public void UpdateOrderDetail(Order o)
        {
            orderDetailDAO.UpdateOrderDetail(o);
        }
    }
}
