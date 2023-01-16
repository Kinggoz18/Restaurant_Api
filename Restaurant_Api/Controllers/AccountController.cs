/*======================================================================
| OrderServices class
|
| Name: OrderServices.cs
|
| Written by: Chigozie Muonagolu - January 2023
|
| Purpose: Contains services available for a Order object.
|
| usage: Used in PaymentControler and other classes that may depend on it.
|
| Description of properties: None
|
|------------------------------------------------------------------
*/
using System;
using MongoDB.Bson;
using Restaurant_Api.Services;
using Restaurant_Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Restaurant_Api.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class AccountController : ControllerBase
	{
		public AccountController()
		{

		}

        //Endpoint to fetch all accounts from the database
        [HttpGet]
		public ActionResult<List<iAccount>> GetAllAccount(string AdminKey)
		{
			return new List<iAccount>();
		}

        //Endpoint to login in an account
        [HttpGet("{id}")]
        public ActionResult<iAccount> LoginAccount()
        {
            return new Customer();
        }

        //Endpoint to delete a particular account from the database
        [HttpDelete("{id}")]
        public void DeleteAccount()
        {
            
        }

        //Endpoint to update a particular account in the database
        [HttpPut("{id}")]
        public void UpdateAccount()
        {

        }

        //Endpoint to add a new account in the database
        [HttpPost]
        public void CreateAccount()
        {

        }
    }
}

