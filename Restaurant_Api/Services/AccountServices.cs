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

namespace Restaurant_Api.Services
{
    //interface for all accounts
    public interface iAccountServices<T> where T : iAccount {
        public static abstract List<T> GetAll(string AdminKey);
        public static abstract T Login(T account);
        public static abstract T Get(ObjectId account);
        public static abstract void Add(T account);
        public static abstract void Update(T account);
        public static abstract void Remove(ObjectId id);
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
        public static List<Customer> GetAll(string AdminKey) {
            return CustomerCollection.Find(_ => true).ToList();
        }
        //Get a customer for a particular id
        public static Customer Get(ObjectId id)
        {
            FilterDefinition<Customer> filter = Builders<Customer>.Filter.Eq("_id", id);
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
        public static void Remove(ObjectId id)
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
            Customer toRemove = Get(account._id);
            if (toRemove == null)
                return;
            FilterDefinition<Customer> filter = Builders<Customer>.Filter.Eq("_id", account._id);
            CustomerCollection.FindOneAndReplace(filter, account);
        }
    }

    //Admin Services
    public class AdminServices
    {
        static IMongoCollection<Admin>? AdminCollection;     //Admin collection
        static ConnectDB? connection;
       
        public AdminServices()
        {
            connection = new ConnectDB();
            AdminCollection = connection.Client.GetDatabase("Drum_Rock_Jerk").GetCollection<Admin>("Admin");
        }
        //Add a new Admin
        public static void Add(Admin account)
        {
            AdminCollection.InsertOne(account);
        }

        //Return an admin from the database
        public static Admin Get(ObjectId id)
        {
            FilterDefinition<Admin> filter = Builders<Admin>.Filter.Eq("_id", id);
            Admin result = AdminCollection.Find(filter).First();
            return result;
        }
        //Return an admin from the database
        public static Admin Login(Admin account)
        {
            FilterDefinition<Admin> filter = Builders<Admin>.Filter.Eq("EmailAddress", account.EmailAddress);
            Admin result = AdminCollection.Find(filter).First();
            return result;
        }

        //Get all the Admin in the database
        public static List<Admin> GetAll(string AdminKey)
        {
            //Check the admin key is in the database here

            return AdminCollection.Find(_ => true).ToList();
        }

        //Remove any account from the database
        public static void Remove(ObjectId id, string Adminkey)
        {
            //Check the type of the account then remove it correctly !Implement
            Admin toRemove = Get(id);
            if (toRemove == null)
                return;
            FilterDefinition<Admin> filter = Builders<Admin>.Filter.Eq("_id", id);
            AdminCollection.FindOneAndDelete(filter);
        }

        //Update a particular admin account
        public static void Update(Admin account, string Adminkey)
        {
            Admin toRemove = Get(account._id);
            if (toRemove == null)
                return;
            FilterDefinition<Admin> filter = Builders<Admin>.Filter.Eq("_id", account._id);
            AdminCollection.FindOneAndReplace(filter, account);
        }
    }

    //Employee services 
    public class EmployeeServices : iAccountServices<Employee>
    {
        static IMongoCollection<Employee>? EmployeeCollection;   //Employee collection
        static ConnectDB? connection;

        public EmployeeServices()
        {
            connection = new ConnectDB();
            EmployeeCollection = connection.Client.GetDatabase("Drum_Rock_Jerk").GetCollection<Employee>("Employee");
        }

        //Add a new 
        public static void Add(Employee account)
        {
            EmployeeCollection.InsertOne(account);
        }

        //Return an Employee from the database
        public static Employee Get(ObjectId id)
        {
            FilterDefinition<Employee> filter = Builders<Employee>.Filter.Eq("_id", id);
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
        public static List<Employee> GetAll(string AdminKey)
        {
            return EmployeeCollection.Find(_ => true).ToList();
        }

        //Remove an Employee from the database
        public static void Remove(ObjectId id)
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
            Employee toRemove = Get(account._id);
            if (toRemove == null)
                return;
            FilterDefinition<Employee> filter = Builders<Employee>.Filter.Eq("_id", account._id);
            EmployeeCollection.FindOneAndReplace(filter, account);
        }
    }
}

