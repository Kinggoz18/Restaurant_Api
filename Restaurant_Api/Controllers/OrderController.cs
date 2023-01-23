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
        public List<Order> Get()
        {
            return OrderServices.GetallOrder();
        }

        [HttpGet("{id}")]
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
        public void Create(Order order)
        {
            OrderServices.CreateOrder(order);
        }

        [HttpPut("{id}")]
        public IActionResult Update(string id, Order orderIn)
        {
            var objectId = ObjectId.Parse(id);
            var order = OrderServices.GetOrder(objectId);

            if (order == null)
            {
                return NotFound();
            }

            OrderServices.UpdateOrder( id , orderIn);
            return NoContent();
        }

        [HttpGet]
        [Route("GetAllCustomer/{AdminId}")]
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
        [Route("Getorders/{id}")]

        public ActionResult<List<Order>> GetOrdersByUser(string userName)
        {
            var orders = OrderServices.GetOrdersByUser(userName);
            if (orders == null)
            {
                return NotFound();
            }
            return orders;
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var order = OrderServices.GetOrder(ObjectId.Parse(id));

            if (order == null)
            {
                return NotFound();
            }

            OrderServices.RemoveOrder(ObjectId.Parse(id));
            return NoContent();
        }
    }
}



