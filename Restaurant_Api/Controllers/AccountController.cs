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
        public ActionResult<List<AccountData<Admin>>> GetAllAccount(string AdminId) => AdminServices.GetAll(AdminId);

        //Endpoint to login in an account
        [HttpPost]
        [Route("LoginAdmin")]
        public ActionResult<AccountData<Admin>> LoginAccount(Login_Credential account) => AdminServices.Login(account);

        //Endpoint to get a particular account details
        [HttpGet]
        [Route("GetAdmin/{id}")]
        public ActionResult<AccountData<Admin>> Get(string id) => AdminServices.Get(id);

        //Endpoint to delete any account from the database
        [HttpDelete]
        [Route("DeleteAdmin/")]
        public void DeleteAccount(string id) => AdminServices.Remove(id);

        //Endpoint to update a particular admin account
        [HttpPut]
        [Route("UpdateAdmin/")]
        public void UpdateAccount(Admin account, string AdminToUpdate_ID) => AdminServices.Update(account, AdminToUpdate_ID);

        //Endpoint to add a new Admin
        [HttpPost]
        [Route("CreateAdmin/")]
        public ActionResult<AccountData<Admin>> CreateAccount(Admin account) => AdminServices.Add(account);
    }

    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        public CustomerController()
        {

        }

        //Endpoint to fetch all accounts from the database
        [HttpGet]
        [Route("GetAllCustomer/{AdminId}")]
        public ActionResult<List<AccountData<Customer>>> GetAllAccount(string AdminId) => CustomerServices.GetAll(AdminId);

        //Endpoint to login in an account
        [HttpPost]
        [Route("LoginCustomer/")]
        public ActionResult<AccountData<Customer>> LoginAccount(Login_Credential account) => CustomerServices.Login(account);

        //Endpoint to get a particular account details
        [HttpGet]
        [Route("GetCustomer/{id}")]
        public ActionResult<AccountData<Customer>> Get(string id) => CustomerServices.Get(id);

        //Endpoint to delete any account from the database
        [HttpDelete]
        [Route("DeleteCustomer/")]
        public void DeleteAccount(string id) => CustomerServices.Remove(id);

        //Endpoint to update a particular admin account
        [HttpPut]
        [Route("UpdateCustomer/")]
        public ActionResult<AccountData<Customer>> UpdateAccount(Customer account, string AccountoUpdate_ID) => CustomerServices.Update(account, AccountoUpdate_ID);

        //Endpoint to add a new Admin
        [HttpPost]
        [Route("CreateCustomer/")]
        public ActionResult<AccountData<Customer>> CreateAccount(Customer account) => CustomerServices.Add(account);
    }

    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        public EmployeeController()
        {

        }

        //Endpoint to fetch all accounts from the database
        [HttpGet]
        [Route("GetAllEmployee/{AdminId}")]
        public ActionResult<List<AccountData<Employee>>> GetAllAccount(string AdminId) => EmployeeServices.GetAll(AdminId);

        //Endpoint to login in an account
        [HttpPost]
        [Route("LoginEmployee/")]
        public ActionResult<AccountData<Employee>> LoginAccount(Login_Credential account) => EmployeeServices.Login(account);

        //Endpoint to get a particular account details
        [HttpGet]
        [Route("GetEmployee/{id}")]
        public ActionResult<AccountData<Employee>> Get(string id) => EmployeeServices.Get(id);

        //Endpoint to delete any account from the database
        [HttpDelete]
        [Route("DeleteEmployee/")]
        public void DeleteAccount(string id) => EmployeeServices.Remove(id);

        //Endpoint to update a particular Employee account
        [HttpPut]
        [Route("UpdateEmployee/")]
        public ActionResult<AccountData<Employee>> UpdateAccount(Employee account, string AccountoUpdate_ID) => EmployeeServices.Update(account, AccountoUpdate_ID);

        //Endpoint to add a Employee Admin
        [HttpPost]
        [Route("CreateEmployee/")]
        public ActionResult<AccountData<Employee>> CreateAccount(Employee account) => EmployeeServices.Add(account);
    }
}

