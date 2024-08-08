using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject
{
    internal interface IOrderDetailRepository
    {
        void addNewOrder(int productId, int Quantity, int memberId);
        List<Order> GetOrderDetail();
        public List<Order> GetOrderDetailsByMemberId(int id);
        public void UpdateOrderDetail(Order o);
        public void DeleteOrderDetail(int id);
        public Order getOrderById(int id);

    }
}
