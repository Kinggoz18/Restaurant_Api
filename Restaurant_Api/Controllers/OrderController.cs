﻿using System;
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
        [Route("GetOrder/")]//returns a particlar users order
        public ActionResult<List<Order>> GetOrder()
        {
            var orders = OrderServices.GetAllOrders();

            if (orders == null)
            {
                return NotFound();
            }

            return orders;
        }






        [HttpPost]
        [Route("CreateOrder/")]
        public ActionResult<Order> Create(Order order)
        {
            return OrderServices.CreateOrder(order);
        }

        [HttpPut]
        [Route("UpdateOrder/")]
        public ActionResult<Order> Update(string id, Order orderIn) => OrderServices.UpdateOrder( id , orderIn);

        [HttpGet]
        [Route("GetAllOrder/{AdminId}")]
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
            var order = OrderServices.GetOrder(Userid);

            if (order == null)
            {
                return NotFound();
            }

            OrderServices.RemoveOrder(Userid);
            return NoContent();
        }
    }
}



