/*======================================================================
| OrderServices class
|
| Name: OrderServices.cs
|
| Written by: Chigozie Muonagolu - January 2023
|
| Purpose: Contains services available for a Order object.
|
| usage: Used in controllers and other classes that may depend on it.
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
    public class AdminController : ControllerBase
    {
        public AdminController()
        {

        }

        //Endpoint to fetch all accounts from the database
        [HttpGet]
        [Route("GetAllAdmin/{AdminId}")]
        public ActionResult<List<Admin>> GetAllAccount(string AdminId) => AdminServices.GetAll(AdminId);

        //Endpoint to login in an account
        [HttpPost]
        [Route("LoginAdmin")]
        public ActionResult<Admin> LoginAccount(Login_Credential account) => AdminServices.Login(account);

        //Endpoint to get a particular account details
        [HttpGet]
        [Route("GetAdmin/{id}")]
        public ActionResult<Admin> Get(string id) => AdminServices.Get(id);

        //Endpoint to delete any account from the database
        [HttpDelete]
        [Route("DeleteAdmin/{id}")]
        public void DeleteAccount(string id) => AdminServices.Remove(id);

        //Endpoint to update a particular admin account
        [HttpPut]
        [Route("UpdateAdmin/")]
        public void UpdateAccount(Admin account) => AdminServices.Update(account);

        //Endpoint to add a new Admin
        [HttpPost]
        [Route("CreateAdmin/")]
        public void CreateAccount(Admin account) => AdminServices.Add(account);
    }

    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        public CustomerController()
        {

        }

        //Endpoint to fetch all accounts from the database
        [HttpGet("{AdminKey}")]
        public ActionResult<List<Customer>> GetAllAccount(string AdminId) => CustomerServices.GetAll(AdminId);

        //Endpoint to login in an account
        [HttpPost("{id}")]
        public ActionResult<Customer> LoginAccount(Customer account) => CustomerServices.Login(account);

        //Endpoint to get a particular account details
        [HttpGet("{id}")]
        public ActionResult<iAccount> Get(string id) => CustomerServices.Get(id);

        //Endpoint to delete any account from the database
        [HttpDelete]
        public void DeleteAccount(string id) => CustomerServices.Remove(id);

        //Endpoint to update a particular admin account
        [HttpPut("{id}")]
        public void UpdateAccount(Customer account) => CustomerServices.Update(account);

        //Endpoint to add a new Admin
        [HttpPost]
        public void CreateAccount(Customer account) => CustomerServices.Add(account);
    }

    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        public EmployeeController()
        {

        }

        //Endpoint to fetch all accounts from the database
        [HttpGet("{AdminKey}")]
        public ActionResult<List<Employee>> GetAllAccount(string AdminId) => EmployeeServices.GetAll(AdminId);

        //Endpoint to login in an account
        [HttpGet]
        public ActionResult<Employee> LoginAccount(Employee account) => EmployeeServices.Login(account);

        //Endpoint to get a particular account details
        [HttpGet("{id}")]
        public ActionResult<iAccount> Get(string id) => EmployeeServices.Get(id);

        //Endpoint to delete any account from the database
        [HttpDelete]
        public void DeleteAccount(string id) => EmployeeServices.Remove(id);

        //Endpoint to update a particular admin account
        [HttpPut("{id}")]
        public void UpdateAccount(Employee account, string AdminKey) => EmployeeServices.Update(account);

        //Endpoint to add a new Admin
        [HttpPost]
        public void CreateAccount(Employee account) => EmployeeServices.Add(account);
    }
}

