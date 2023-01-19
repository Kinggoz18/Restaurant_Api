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
            var order = OrderServices.GetOrder(ObjectId.Parse(id));

            if (order == null)
            {
                return NotFound();
            }

            OrderServices.UpdateOrder(string id , orderIn);
            return NoContent();
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



