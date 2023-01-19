 /*======================================================================
| AccountServices class
|
| Name: AccountServices.cs
|
| Written by: Chigozie Muonagolu - January 2023
|
| Purpose: Contains services available for an Account object.
|
| usage: Used in AccountControler and other classes that may depend on it.
|
| Description of properties: None
|
|------------------------------------------------------------------
*/
using System;
using ConnectDatabase;
using MongoDB.Driver;
using MongoDB.Bson;
using Restaurant_Api.Models;
using System.Security.Cryptography;

namespace Restaurant_Api.Services
{
    //interface for all accounts
    public interface iAccountServices<T> where T : iAccount {
        public static abstract List<T> GetAll(string AdminId);
        public static abstract T Login(T account);
        public static abstract T Get(string account);
        public static abstract void Add(T account);
        public static abstract void Update(T account);
        public static abstract void Remove(string id);
    }

    //Customer Services
    public class CustomerServices : iAccountServices<Customer>
	{
         static ConnectDB connection = new ConnectDB();
        static IMongoCollection<Customer> CustomerCollection = connection.Client.GetDatabase("Drum_Rock_Jerk").GetCollection<Customer>("Customer");   //Customer collection

        public CustomerServices()

		{
			
        }

        //Get All the customers in the Database
        public static List<Customer> GetAll(string AdminId) {
            Admin current = AdminServices.Get(AdminId);
            if (current == null)
                return new List<Customer>();

            return CustomerCollection.Find(_ => true).ToList();
        }
        //Get a customer for a particular id
        public static Customer Get(string id)
        {
            ObjectId SearchId = new ObjectId(id);
            FilterDefinition<Customer> filter = Builders<Customer>.Filter.Eq("_id", SearchId);
            Customer result = CustomerCollection.Find(filter).First();
            return result;
        }
        public static Customer Login(Customer account)
        {
            FilterDefinition<Customer> filter = Builders<Customer>.Filter.Eq("EmailAddress", account.EmailAddress);
            //Check the password matches here 
            Customer result = CustomerCollection.Find(filter).First();
            return result;
        }
        //Add Customer
        public static void Add(Customer account) {
            CustomerCollection.InsertOne(account);
        }
        //Delete Customer
        public static void Remove(string id)
        {
            Customer toRemove = Get(id);
            if (toRemove == null)
                return;
            FilterDefinition<Customer> filter = Builders<Customer>.Filter.Eq("_id", id);
            CustomerCollection.FindOneAndDelete(filter);
        }
        //Update a customer
        public static void Update(Customer account)
        {
            Customer toRemove = Get(account._id.ToString());
            if (toRemove == null)
                return;
            FilterDefinition<Customer> filter = Builders<Customer>.Filter.Eq("_id", account._id);
            CustomerCollection.FindOneAndReplace(filter, account);
        }
    }

    //Admin Services
    public class AdminServices
    {
        static ConnectDB connection  = new ConnectDB();
        static IMongoCollection<Admin> AdminCollection = connection.Client.GetDatabase("Drum_Rock_Jerk").GetCollection<Admin>("Admin");    //Admin collection
       
        public AdminServices()
        {
            
            
        }
        //Add a new Admin
        public static void Add(Admin account)
        {
            AdminCollection.InsertOne(account);
        }

        //Return an admin from the database
        public static Admin Get(string id)
        {
            ObjectId SearchId = new ObjectId(id);
            FilterDefinition<Admin> filter = Builders<Admin>.Filter.Eq("_id", SearchId);
            Admin result;
            try
            {
                result = AdminCollection.Find(filter).First();
            }
            catch (InvalidOperationException ex)
            {

                Console.WriteLine("Inalid Admin ID passed.");
                return null;
            }
            return result;

        }
        //Return an admin from the database
        public static Admin Login(Login_Credential account)
        {
            FilterDefinition<Admin> filter = Builders<Admin>.Filter.Eq("EmailAddress", account.EmailAddress);
            if (filter == null)
                return new Admin();
            Admin result = AdminCollection.Find(filter).First();
            return result;
        }

        //Get all the Admin in the database
        public static List<Admin> GetAll(string AdminId)
        {
            Admin current = AdminServices.Get(AdminId);
            if (current == null)
                return new List<Admin>();
            return AdminCollection.Find(_ => true).ToList();
        }

        //Remove any account from the database
        public static void Remove(string id)
        {
            //Check the type of the account then remove it correctly !Implement
            Admin toRemove = Get(id);
            if (toRemove == null)
                return;
            FilterDefinition<Admin> filter = Builders<Admin>.Filter.Eq("_id", toRemove._id);;
            AdminCollection.FindOneAndDelete(filter);
        }

        //Update a particular admin account
        public static void Update(Admin account)
        {
            Admin toUpdate = Get(account._id.ToString());
            if (toUpdate == null)
                return;
            FilterDefinition<Admin> filter = Builders<Admin>.Filter.Eq("_id", account._id);
            AdminCollection.FindOneAndReplace(filter, toUpdate);
        }
    }

    //Employee services
    public class EmployeeServices : iAccountServices<Employee>
    {
        static ConnectDB? connection = new ConnectDB();
        static IMongoCollection<Employee>? EmployeeCollection = connection.Client.GetDatabase("Drum_Rock_Jerk").GetCollection<Employee>("Employee");   //Employee collection

        public EmployeeServices()
        {

        }

        //Add a new
        public static void Add(Employee account)
        {
            EmployeeCollection.InsertOne(account);
        }

        //Return an Employee from the database
        public static Employee Get(string id)
        {
            ObjectId SearchId = new ObjectId(id);
            FilterDefinition<Employee> filter = Builders<Employee>.Filter.Eq("_id", SearchId);
            Employee result = EmployeeCollection.Find(filter).First();
            return result;
        }
        //Return an Employee from the database
        public static Employee Login(Employee account)
        {
            FilterDefinition<Employee> filter = Builders<Employee>.Filter.Eq("EmailAddress", account.EmailAddress);
            Employee result = EmployeeCollection.Find(filter).First();
            return result;
        }
        //Get all the Employee in the database
        public static List<Employee> GetAll(string AdminId)
        {
            Admin current = AdminServices.Get(AdminId);
            if (current == null)
                return new List<Employee>();
            return EmployeeCollection.Find(_ => true).ToList();
        }

        //Remove an Employee from the database
        public static void Remove(string id)
        {
            Employee toRemove = Get(id);
            if (toRemove == null)
                return;
            FilterDefinition<Employee> filter = Builders<Employee>.Filter.Eq("_id", id);
            EmployeeCollection.FindOneAndDelete(filter);
        }

        //Update a particular Employee account
        public static void Update(Employee account)
        {
            Employee toRemove = Get(account._id.ToString());
            if (toRemove == null)
                return;
            FilterDefinition<Employee> filter = Builders<Employee>.Filter.Eq("_id", account._id);
            EmployeeCollection.FindOneAndReplace(filter, account);
        }
    }
}
