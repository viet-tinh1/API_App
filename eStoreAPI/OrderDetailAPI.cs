using BusinessObject.Models;
using DataAccess.Repositories;
using eStoreAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace eStoreAPI
{
    [Route("api/orderdetail")]
    [ApiController]
    public class OrderDetailAPI : ControllerBase
    {
        private OrderDetailRepository oderDetailRepository = new OrderDetailRepository();
        [HttpPost("addOrder")]
        public IActionResult addOrder([FromBody] OrderModel o)
        {
            oderDetailRepository.addNewOrder(o.ProductId, o.Quantity, o.MemberId);
            return NoContent();
        }
        [HttpGet("GetOrderDetail")]
        public ActionResult<IEnumerable<Order>> GetOrderDetail()
        {
            return oderDetailRepository.GetOrderDetail();
        }
        [HttpGet("GetOrderDetailsByMemberId")]
        public ActionResult<IEnumerable<Order>> GetOrderDetailsByMemberId(int id)
        {
            return oderDetailRepository.GetOrderDetailsByMemberId(id);
        }
        [HttpGet("DeleteOrderDetail")]
        public IActionResult DeleteOrderDetail(int id)
        {
            oderDetailRepository.DeleteOrderDetail(id);
            return NoContent();
        }
        [HttpPost("UpdateOrderDetail")]
        public IActionResult UpdateOrderDetail(Order o)
        {
            Order order = new Order()
            {
                OrderId = o.OrderId,
                OrderDate = o.OrderDate,
                RequiredDate = o.RequiredDate,
                ShippedDate = o.ShippedDate
            };

            oderDetailRepository.UpdateOrderDetail(order);
            return NoContent();
        }
        [HttpGet("getOrderById")]
        public ActionResult<Order> getProductById(int id)
        {
            return oderDetailRepository.getOrderById(id);
        }
    }
}
