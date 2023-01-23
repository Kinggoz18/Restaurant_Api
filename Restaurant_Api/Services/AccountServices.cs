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
using System.Net;
using System.Text;

namespace Restaurant_Api.Services
{
    //interface for all accounts
    public interface iAccountServices<T> where T : iAccount {
        public static abstract List<T> GetAll(string AdminId);
        public static abstract T Login(Login_Credential account);
        public static abstract T Get(string account);
        public static abstract T Add(T account);
        public static abstract T Update(T account, string AccountoUpdate_ID);
        public static abstract void Remove(string id);
    }
    //Class for hashing users passwords
    public class EncryptPassword
    {
        public EncryptPassword() { }

        public static string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
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
            try
            {

                Admin current = AdminServices.Get(AdminId);
                if (current == null)
                    return new List<Customer>();

                return CustomerCollection.Find(_ => true).ToList();
            }
            catch
            {
                Console.WriteLine("Inalid body response passed.");
                return null;
            }
        }
        //Get a customer for a particular id
        public static Customer Get(string id)
        {
            try
            {

                ObjectId SearchId = new ObjectId(id);
                FilterDefinition<Customer> filter = Builders<Customer>.Filter.Eq("_id", SearchId);
                Customer result = CustomerCollection.Find(filter).FirstOrDefault();
                return result;
            }
            catch 
            {
                Console.WriteLine("Inalid body response passed.");
                return null;
            }
        }
        public static Customer Login(Login_Credential account)
        {
            try
            {
                FilterDefinition<Customer> filter = Builders<Customer>.Filter.Eq("EmailAddress", account.EmailAddress);
                if (filter == null)
                    return new Customer();
                Customer result = CustomerCollection.Find(filter).FirstOrDefault();
                if (result != null)
                {
                    if (EncryptPassword.HashPassword(account.Password) == result.Password)
                    {
                        return result;
                    }
                }
                return null;
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine("Inalid body response passed.");
                return null;
            }
            catch
            {
                Console.WriteLine("Inalid body response passed.");
                return null;
            }
        }
        //Add Customer
        public static Customer Add(Customer account) {
            try
            {
                //Find if an account with the existing email already exists
                FilterDefinition<Customer> filter = Builders<Customer>.Filter.Eq("EmailAddress", account.EmailAddress);
                Customer result = CustomerCollection.Find(filter).FirstOrDefault();
                if (result != null)
                {
                    return null;
                }
                //Hash the password
                account.Password = EncryptPassword.HashPassword(account.Password);
                CustomerCollection.InsertOne(account);
                return account;
            }
            catch(InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            catch
            {
                Console.WriteLine("Inalid body response passed.");
                return null;
            }
        }
        //Delete Customer
        public static void Remove(string id)
        {
            try
            {

                Customer toRemove = Get(id);
                if (toRemove == null)
                    return;
                FilterDefinition<Customer> filter = Builders<Customer>.Filter.Eq("_id", toRemove._id);
                CustomerCollection.FindOneAndDelete(filter);
            }
            catch
            {
                Console.WriteLine("Inalid body response passed.");
                return;
            }
        }
        //Update a customer
        public static Customer Update(Customer account, string AccountoUpdate_ID)
        {
            try
            {

                Customer toUpdate = Get(AccountoUpdate_ID);
                if (toUpdate == null)
                    return null;
                account._id = toUpdate._id;
                FilterDefinition<Customer> filter = Builders<Customer>.Filter.Eq("_id", toUpdate._id);
                account.Password = EncryptPassword.HashPassword(account.Password);  //Encrypt the password again
                CustomerCollection.FindOneAndReplace(filter, account);
                return account;
            }
            catch 
            {
                Console.WriteLine("Inalid body response passed.");
                return null;
            }
        }
        //Endpoint to update a customers point, after placing an order
        public static Customer UpdatePoint(string AccountoUpdate_ID)
        {
            try
            {
                Customer accountToUpdate = Get(AccountoUpdate_ID);
                FilterDefinition<Customer> filter = Builders<Customer>.Filter.Eq("_id", accountToUpdate._id);
                var update = Builders<Customer>.Update.Set("Points", accountToUpdate.Points + 10);  //Increment points by 10
                CustomerCollection.UpdateOne(filter, update);
                return accountToUpdate;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        //Endpoint to consume customers point
        public static Customer UsePoints(string AccountoUpdate_ID)
        {
            try
            {
                Customer accountToUpdate = Get(AccountoUpdate_ID);
                FilterDefinition<Customer> filter = Builders<Customer>.Filter.Eq("_id", accountToUpdate._id);
                if(accountToUpdate.Points >= 50) {
                    var update = Builders<Customer>.Update.Set("Points", accountToUpdate.Points - 50);  //Remove 50 points from the account
                    CustomerCollection.UpdateOne(filter, update);
                    return accountToUpdate;
                }
                else
                {
                    return accountToUpdate;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
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
        public static Admin Add(Admin account)
        {
            try
            {
                //Find if an account with the existing email already exists
                FilterDefinition<Admin> filter = Builders<Admin>.Filter.Eq("EmailAddress", account.EmailAddress);
                Admin result = AdminCollection.Find(filter).FirstOrDefault();
                if (result != null)
                {
                    return null;
                }
                //Hash the password
                account.Password = EncryptPassword.HashPassword(account.Password);  //Hash the password
                AdminCollection.InsertOne(account);
                return result = AdminCollection.Find(filter).FirstOrDefault();
            }
            catch
            {
                Console.WriteLine("An Exception occured!");
                return null;
            }
        }

        //Return an admin from the database
        public static Admin Get(string id)
        {
            try
            {
                ObjectId SearchId = new ObjectId(id);
                FilterDefinition<Admin> filter = Builders<Admin>.Filter.Eq("_id", SearchId);
                Admin result;
                result = AdminCollection.Find(filter).FirstOrDefault();
                return result;
            }
            catch (InvalidOperationException ex)
            {

                Console.WriteLine("Inalid Admin ID passed.");
                return null;
            }
            catch
            {
                Console.WriteLine("An Exception occured!");
                return null;
            }


        }
        //Return an admin from the database
        public static Admin Login(Login_Credential account)
        {
            FilterDefinition<Admin> filter = Builders<Admin>.Filter.Eq("EmailAddress", account.EmailAddress);
            try
            {
                Admin result = AdminCollection.Find(filter).FirstOrDefault();
                if (result != null)
                {
                    if (EncryptPassword.HashPassword(account.Password) == result.Password)
                    {
                        return result;
                    }
                }
                return null;
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine("Inalid body response passed.");
                return null;
            }
            catch
            {
                Console.WriteLine("An Exception occured!");
                return null;
            }
        }

        //Get all the Admin in the database
        public static List<Admin> GetAll(string AdminId)
        {
            try
            {

                Admin current = AdminServices.Get(AdminId);
                if (current == null)
                    return new List<Admin>();
                return AdminCollection.Find(_ => true).ToList();
            }
            catch 
            {
                Console.WriteLine("An Exception occured!");
                return null;
            }
        }

        //Remove any account from the database
        public static void Remove(string id)
        {
            try
            {
                 //Check the type of the account then remove it correctly !Implement
                Admin toRemove = Get(id);
                if (toRemove == null)
                    return;
                FilterDefinition<Admin> filter = Builders<Admin>.Filter.Eq("_id", toRemove._id); ;
                AdminCollection.FindOneAndDelete(filter);
            }
            catch
            {
                Console.WriteLine("An Exception occured!");
                return;
            }
        }

        //Update a particular admin account
        public static void Update(Admin account, string AdminToUpdate_ID)
        {
            try
            {

                Admin toUpdate = Get(AdminToUpdate_ID);
                if (toUpdate == null)
                    return;
                account._id = toUpdate._id;
                FilterDefinition<Admin> filter = Builders<Admin>.Filter.Eq("_id", toUpdate._id);
                account.Password = EncryptPassword.HashPassword(account.Password);  //Encrypt the password again
                AdminCollection.FindOneAndReplace(filter, account);
            }
            catch
            {
                Console.WriteLine("An Exception occured!");

            }
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
        public static Employee Add(Employee account)
        {
            try
            {
                //Find if an account with the existing email already exists
                FilterDefinition<Employee> filter = Builders<Employee>.Filter.Eq("EmailAddress", account.EmailAddress);
                Employee result = EmployeeCollection.Find(filter).FirstOrDefault();
                if (result != null)
                {
                    return null;
                }
                //Hash the password
                account.Password = EncryptPassword.HashPassword(account.Password);
                EmployeeCollection.InsertOne(account);
                return result = EmployeeCollection.Find(filter).FirstOrDefault();
            }
            catch
            {
                Console.WriteLine("An Exception occured!");
                return null;
            }
        }

        //Return an Employee from the database
        public static Employee Get(string id)
        {
            try
            {

                ObjectId SearchId = new ObjectId(id);
                FilterDefinition<Employee> filter = Builders<Employee>.Filter.Eq("_id", SearchId);
                Employee result = EmployeeCollection.Find(filter).FirstOrDefault();
                return result;
            }
            catch
            {
                Console.WriteLine("An Exception occured!");
                return null;

            }
        }
        //Return an Employee from the database
        public static Employee Login(Login_Credential account)
        {
            try
            {

                FilterDefinition<Employee> filter = Builders<Employee>.Filter.Eq("EmailAddress", account.EmailAddress);
                if (filter == null)
                    return null;
                Employee result = EmployeeCollection.Find(filter).FirstOrDefault();
                if (result != null)
                {
                    if (EncryptPassword.HashPassword(account.Password) == result.Password)
                    {
                        return result;
                    }
                }
                return null;
                
                
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine("Inalid body response passed.");
                return null;
            }
            catch
            {
                Console.WriteLine("An Exception occured!");
                return null;
            }

        }
        //Get all the Employee in the database
        public static List<Employee> GetAll(string AdminId)
        {
            try
            {

                Admin current = AdminServices.Get(AdminId);
                if (current == null)
                    return new List<Employee>();
                return EmployeeCollection.Find(_ => true).ToList();
            }
            catch
            {
                Console.WriteLine("Inalid body response passed.");
                return null;
            }
        }

        //Remove an Employee from the database
        public static void Remove(string id)
        {
            try
            {

                Employee toRemove = Get(id);
                if (toRemove == null)
                    return;
                FilterDefinition<Employee> filter = Builders<Employee>.Filter.Eq("_id", toRemove._id);
                EmployeeCollection.FindOneAndDelete(filter);
            }
            catch
            {
                Console.WriteLine("Inalid body response passed.");
                return;
            }
        }

        //Update a particular Employee account
        public static Employee Update(Employee account, string AccountoUpdate_ID)
        {
            try
            {

                Employee toUpdate = Get(AccountoUpdate_ID);
                if (toUpdate == null)
                    return null;
                account._id = toUpdate._id;
                FilterDefinition<Employee> filter = Builders<Employee>.Filter.Eq("_id", toUpdate._id);
                account.Password = EncryptPassword.HashPassword(account.Password);  //Encrypt the password again
                EmployeeCollection.FindOneAndReplace(filter, account);
                return account;
            }
            catch
            {
                Console.WriteLine("Inalid body response passed.");
                return null;
            }
        }
    }
}
