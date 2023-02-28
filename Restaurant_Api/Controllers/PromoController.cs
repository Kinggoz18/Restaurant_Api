using System;
using Microsoft.AspNetCore.Mvc;
using Restaurant_Api.Models;
using Restaurant_Api.Services;

namespace Restaurant_Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PromoController : ControllerBase
	{
		public PromoController()
		{

        }
        //Endpoint to add a promo to the database
        [HttpPut]
        [Route("AddPromo/")]
        public ActionResult<string> AdddPromo(Promo toAdd)
        {
            string result = PromoServices.AddPromo(toAdd);
            if (result == "Inavlid")
            {
                return NotFound();  //return a bad request
            }
            else
            {
                return result;
            }
        }


        //Endpoint to remove a promo
        [HttpDelete]
        [Route("DeletePromo/{promo}")]
        public ActionResult<int> DeletePromo(string promo)
        {
            int result = PromoServices.RemovePromo(promo);
            if (result == -1)
            {
                return StatusCodes.Status400BadRequest; //return a bad request
            }
            else
            {
                return StatusCodes.Status200OK; //Retrurn request successful
            }
        }

        //Endpoint find if a promo is in the database
        [HttpPost]
        [Route("IsValidPromo/{promo}")]
        public ActionResult<int> CheckPromo(string promo)
        {
            bool result = PromoServices.CheckPromo(promo);
            if (!result)
            {
                return StatusCodes.Status400BadRequest; //return a bad request
            }
            else
            {
                return StatusCodes.Status200OK; //Retrurn request successful
            }
        }

        //Endpoint to return all the promos in the database
        [HttpGet]
        [Route("GetAllPromo/{AdminId}")]
        public ActionResult<List<Promo>> GetAllPromo(string AdminId)
        {
            var results = PromoServices.GetAllPromo(AdminId);
            if(results == null){
                return NotFound();
            }
            else
            {
                return results;
            }
        }
    }
}

