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
        public static abstract List<T> GetAll();
        public static abstract T Get(ObjectId id);
        public static abstract void Add(T account);
        public static abstract void Update(T account);
        public static abstract void Remove(ObjectId id);
    }

    //Customer Services
    public class CustomerServices : iAccountServices<Customer>
	{


		public AccountServices()

        static IMongoCollection<Customer> CustomerCollection;   //Customer collection
        static ConnectDB? connection;

        public CustomerServices()

		{
			connection = new ConnectDB();
            CustomerCollection = connection.Client.GetDatabase("Drum_Rock_Jerk").GetCollection<Customer>("Customer");
        }

        //Get All the customers in the Database
        public static List<Customer> GetAll() {
            return CustomerCollection.Find(_ => true).ToList();
        }
        //Get a customer for a particular id
        public static Customer Get(ObjectId id)
        {
            FilterDefinition<Customer> filter = Builders<Customer>.Filter.Eq("_id", id);
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
            Customer toRemove = Get(account._Id);
            if (toRemove == null)
                return;
            FilterDefinition<Customer> filter = Builders<Customer>.Filter.Eq("_id", account._Id);
            CustomerCollection.FindOneAndReplace(filter, account);
        }
    }

    //Admin Services
    public class AdminServices : iAccountServices<Admin>
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

        //Get all the admin in the database
        public static List<Admin> GetAll()
        {
            return AdminCollection.Find(_ => true).ToList();
        }

        //Remove an admin from the database
        public static void Remove(ObjectId id)
        {
            Admin toRemove = Get(id);
            if (toRemove == null)
                return;
            FilterDefinition<Admin> filter = Builders<Admin>.Filter.Eq("_id", id);
            AdminCollection.FindOneAndDelete(filter);
        }

        //Update a particular admin account
        public static void Update(Admin account)
        {
            Admin toRemove = Get(account._Id);
            if (toRemove == null)
                return;
            FilterDefinition<Admin> filter = Builders<Admin>.Filter.Eq("_id", account._Id);
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

        //Get all the Employee in the database
        public static List<Employee> GetAll()
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
            Employee toRemove = Get(account._Id);
            if (toRemove == null)
                return;
            FilterDefinition<Employee> filter = Builders<Employee>.Filter.Eq("_id", account._Id);
            EmployeeCollection.FindOneAndReplace(filter, account);
        }
    }
}
