using System;
using Microsoft.AspNetCore.Mvc;
using Restaurant_Api.Services;

namespace Restaurant_Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SalesController : ControllerBase
    {
        public SalesController()
        {

        }
        [HttpGet("GetTotalSales(Price)/{GetTotalSales}")]
        public IActionResult GetTotalSales()
        {
            // Get all orders and calculate total sales
            var orders = OrderServices.GetAllOrders();
            var totalSales = orders.Sum(x => x.TotalPrice);

            return Ok(new { TotalSales = totalSales });
        }
        [HttpGet("GetNumberOfOrders/{GetNumberOfOrders}")]
        public IActionResult GetNumberOfOrders()
        {
            // Get number of orders
            var numberOfOrders = OrderServices.GetAllOrders().Count;

            return Ok(new { NumberOfOrders = numberOfOrders });
        }
    }
}

