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
        [HttpGet]
        public ActionResult<double> GetTotalSales()
        {
            var orders = OrderServices.GetAllOrders();
            var totalSales = orders.Sum(x => x.TotalPrice);
            return Ok(totalSales);
        }
    }
}

