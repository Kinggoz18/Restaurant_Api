using System;
using Microsoft.AspNetCore.Mvc;
using Restaurant_Api.Services;
using Restaurant_Api.Models;

namespace Restaurant_Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SalesController : ControllerBase
    {
        public SalesController()
        {

        }
        [HttpGet("GetSales/")]
        public ActionResult<Sales> GetTotalSales()
        {
            // Get number of orders
            var numberOfOrders = OrderServices.GetAllOrders().Count;

            // Get all orders and calculate total sales
            var orders = OrderServices.GetAllOrders();
            var totalSales = orders.Sum(x => x.TotalPrice);

            return new Sales { GetTotalSales = totalSales, GetNumberOfOrders = numberOfOrders };
        }
    }
}

