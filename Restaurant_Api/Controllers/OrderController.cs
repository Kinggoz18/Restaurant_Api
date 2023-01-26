using System;
using MongoDB.Bson;
using Restaurant_Api.Services;
using Restaurant_Api.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace Restaurant_Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {

        public OrdersController()
        {

        }

        

        [HttpGet]
        [Route("GetOrder")]//returns a particlar users order
        public ActionResult<Order> Get(string id)
        {
            var order = OrderServices.GetOrder(ObjectId.Parse(id));

            if (order == null)
            {
                return NotFound();
            }
           

            return order;
        }
        

        
        

        [HttpPost]
        [Route("CreateOrder/")]
        public void Create(Order order)
        {
            OrderServices.CreateOrder(order);
        }

        [HttpPut]
        [Route("UpdateOrder/")]
        public ActionResult<Order> Update(string id, Order orderIn) => OrderServices.UpdateOrder( id , orderIn);

        [HttpGet]
        [Route("GetAllUsers/{AdminId}")]
        public ActionResult<List<Order>> GetAllOrders()
        {
            var orders = OrderServices.GetAllOrders();
            if (orders == null)
            {
                return NotFound();
            }
            return orders;
        }

        // GET api/orders/username
        [HttpGet]
        [Route("GetOrders/{id}")]

        public ActionResult<List<Order>> GetOrdersByUser(string userName)
        {
            var orders = OrderServices.GetOrdersByUser(userName);
            if (orders == null)
            {
                return NotFound();
            }
            return orders;
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public IActionResult Delete(string Userid)
        {
            var order = OrderServices.GetOrder(ObjectId.Parse(Userid));

            if (order == null)
            {
                return NotFound();
            }

            OrderServices.RemoveOrder(ObjectId.Parse(Userid));
            return NoContent();
        }
    }
}



