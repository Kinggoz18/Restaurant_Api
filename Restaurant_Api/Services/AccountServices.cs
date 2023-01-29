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
using System.Collections.Generic;
using CloudinaryDotNet;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Restaurant_Api.Services
{
    //interface for all accounts
    public interface iAccountServices<T> where T : iAccount {
        public static abstract List<AccountData<T>> GetAll(string AdminId);
        public static abstract AccountData<T> Login(Login_Credential account);
        public static abstract AccountData<T> Get(string account);
        public static abstract AccountData<T> Add(T account);
        public static abstract AccountData<T> Update(T account, string AccountoUpdate_ID);
        public static abstract void Remove(string id);
    }
    public class AccountData<T> where T: iAccount
    {
        public string? _id;
        public T? account;
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
        public static List<AccountData<Customer>> GetAll(string AdminId) {
            try
            {
                AccountData<Admin> current = AdminServices.Get(AdminId);
                if (current == null)
                    return new List<AccountData<Customer>>();

                List<Customer> temp = CustomerCollection.Find(_ => true).ToList();
                List<AccountData<Customer>> final = new List<AccountData<Customer>>();
                foreach (Customer x in temp)
                {
                    AccountData<Customer> tempData = new AccountData<Customer>();
                    tempData.account = x;
                    tempData._id = x._id.ToString();
                    final.Add(tempData);
                }
                return final;
            }
            catch
            {
                Console.WriteLine("Inalid body response passed.");
                return null;
            }
        }
        //Get a customer for a particular id
        public static AccountData<Customer> Get(string id)
        {
            try
            {
                ObjectId SearchId = new ObjectId(id);
                FilterDefinition<Customer> filter = Builders<Customer>.Filter.Eq("_id", SearchId);
                if (filter == null)
                    return null;

                Customer result = CustomerCollection.Find(filter).FirstOrDefault();
                AccountData<Customer> tempData = new AccountData<Customer>();
                tempData.account = result;
                tempData._id = result._id.ToString();
                return tempData;
            }
            catch 
            {
                Console.WriteLine("Inalid body response passed.");
                return null;
            }
        }
        public static AccountData<Customer> Login(Login_Credential account)
        {
            try
            {
                FilterDefinition<Customer> filter = Builders<Customer>.Filter.Eq("EmailAddress", account.EmailAddress);
                if (filter == null)
                    return null;
                Customer result = CustomerCollection.Find(filter).FirstOrDefault();
                if (result != null)
                {
                    if (EncryptPassword.HashPassword(account.Password) == result.Password)
                    {

                        AccountData<Customer> tempData = new AccountData<Customer>();
                        tempData.account = result;
                        tempData._id = result._id.ToString();
                        return tempData;
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
        public static AccountData<Customer> Add(Customer account) {
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

                AccountData<Customer> tempData = new AccountData<Customer>();
                tempData.account = account;
                tempData._id = account._id.ToString();
                return tempData;
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
                AccountData<Customer> toRemove = Get(id);
                if (toRemove == null)
                    return;
                FilterDefinition<Customer> filter = Builders<Customer>.Filter.Eq("_id", toRemove.account._id);
                CustomerCollection.FindOneAndDelete(filter);
            }
            catch
            {
                Console.WriteLine("Inalid body response passed.");
                return;
            }
        }
        //Update a customer
        public static AccountData<Customer> Update(Customer account, string accountoUpdate_ID)
        {
            try
            {

                AccountData<Customer> toUpdate = Get(accountoUpdate_ID);
                if (toUpdate == null)
                    return null;
                account._id = toUpdate.account._id;
                FilterDefinition<Customer> filter = Builders<Customer>.Filter.Eq("_id", toUpdate.account._id);
                account.Password = toUpdate.account.Password;  //copy the old password again
                CustomerCollection.FindOneAndReplace(filter, account);

                AccountData<Customer> data = new AccountData<Customer>();
                data.account = account;
                data._id = account._id.ToString();
                return data;
            }
            catch 
            {
                Console.WriteLine("Inalid body response passed.");
                return null;
            }
        }
        //Endpoint to update a customers point, after placing an order
        public static AccountData<Customer> UpdatePoint(string AccountoUpdate_ID)
        {
            try
            {
                AccountData<Customer> accountToUpdate = Get(AccountoUpdate_ID);
                FilterDefinition<Customer> filter = Builders<Customer>.Filter.Eq("_id", accountToUpdate._id);
                var update = Builders<Customer>.Update.Set("Points", accountToUpdate.account.Points + 10);  //Increment points by 10
                CustomerCollection.UpdateOne(filter, update);

                return Get(accountToUpdate._id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        //Endpoint to consume customers point
        public static AccountData<Customer> UsePoints(string AccountoUpdate_ID)
        {
            try
            {
                AccountData<Customer> accountToUpdate = Get(AccountoUpdate_ID);
                FilterDefinition<Customer> filter = Builders<Customer>.Filter.Eq("_id", accountToUpdate._id);
                if(accountToUpdate.account.Points >= 50) {
                    var update = Builders<Customer>.Update.Set("Points", accountToUpdate.account.Points - 50);  //Remove 50 points from the account
                    CustomerCollection.UpdateOne(filter, update);
                    return Get(accountToUpdate._id);
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
        public static AccountData<Admin> Add(Admin account)
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
                AccountData<Admin> data = new AccountData<Admin>();
                data.account = AdminCollection.Find(filter).FirstOrDefault();
                data._id = data.account._id.ToString();
                return data;
            }
            catch
            {
                Console.WriteLine("An Exception occured!");
                return null;
            }
        }

        //Return an admin from the database
        public static AccountData<Admin> Get(string id)
        {
            try
            {
                ObjectId SearchId = new ObjectId(id);
                FilterDefinition<Admin> filter = Builders<Admin>.Filter.Eq("_id", SearchId);
                Admin result;
                AccountData<Admin> data = new AccountData<Admin>();
                data.account = AdminCollection.Find(filter).FirstOrDefault();
                data._id = data.account._id.ToString();
                return data;
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
        public static AccountData<Admin> Login(Login_Credential account)
        {
            FilterDefinition<Admin> filter = Builders<Admin>.Filter.Eq("EmailAddress", account.EmailAddress);
            try
            {
                Admin result = AdminCollection.Find(filter).FirstOrDefault();
                if (result != null)
                {
                    if (EncryptPassword.HashPassword(account.Password) == result.Password)
                    {
                        AccountData<Admin> data = new AccountData<Admin>();
                        data.account = result;
                        data._id = result._id.ToString();
                        return data;
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
        public static List<AccountData<Admin>> GetAll(string AdminId)
        {
            try
            {
                List<AccountData<Admin>> data = new List<AccountData<Admin>>();
                AccountData<Admin> current = AdminServices.Get(AdminId);
                if (current == null)
                    return null;
                List<Admin> temp = AdminCollection.Find(_ => true).ToList();
                foreach(Admin a in temp)
                {
                    AccountData<Admin> curr = new AccountData<Admin>();
                    curr.account = a;
                    curr._id = a._id.ToString();
                    data.Add(curr);
                }
                return data;
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
                AccountData<Admin> toRemove = Get(id);
                if (toRemove == null)
                    return;
                FilterDefinition<Admin> filter = Builders<Admin>.Filter.Eq("_id", toRemove.account._id); ;
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
                AccountData<Admin> toUpdate = Get(AdminToUpdate_ID);
                if (toUpdate == null)
                    return;
                account._id = toUpdate.account._id;
                FilterDefinition<Admin> filter = Builders<Admin>.Filter.Eq("_id", toUpdate.account._id);
                account.Password = toUpdate.account.Password;  //copy the old password
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

        //Adds a new Employee
        public static AccountData<Employee> Add(Employee account)
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
                AccountData<Employee> data = new AccountData<Employee>();
                result = EmployeeCollection.Find(filter).FirstOrDefault();
                data.account = result;
                data._id = result._id.ToString();
                return data;
            }
            catch
            {
                Console.WriteLine("An Exception occured!");
                return null;
            }
        }

        //Return an Employee from the database
        public static AccountData<Employee> Get(string id)
        {
            try
            {

                ObjectId SearchId = new ObjectId(id);
                FilterDefinition<Employee> filter = Builders<Employee>.Filter.Eq("_id", SearchId);
                Employee result = EmployeeCollection.Find(filter).FirstOrDefault();
                AccountData<Employee> data = new AccountData<Employee>();
                data._id = result._id.ToString();
                data.account = result;
                return data;
            }
            catch
            {
                Console.WriteLine("An Exception occured!");
                return null;

            }
        }
        //Return an Employee from the database
        public static AccountData<Employee> Login(Login_Credential account)
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
                        AccountData<Employee> data = new AccountData<Employee>();
                        data.account = result;
                        data._id = result._id.ToString();
                        return data;
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
        public static List<AccountData<Employee>> GetAll(string AdminId)
        {
            try
            {
                List<AccountData<Employee>> data = new List<AccountData<Employee>>();
                AccountData<Admin> current = AdminServices.Get(AdminId);
                if (current == null)
                    return null;
                List<Employee> temp = EmployeeCollection.Find(_ => true).ToList();
                foreach (Employee a in temp)
                {
                    AccountData<Employee> curr = new AccountData<Employee>();
                    curr.account = a;
                    curr._id = a._id.ToString();
                    data.Add(curr);
                }
                return data;
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
                AccountData<Employee> toRemove = Get(id);
                if (toRemove == null)
                    return;
                FilterDefinition<Employee> filter = Builders<Employee>.Filter.Eq("_id", toRemove.account._id);
                EmployeeCollection.FindOneAndDelete(filter);
            }
            catch
            {
                Console.WriteLine("Inalid body response passed.");
                return;
            }
        }

        //Update a particular Employee account
        public static AccountData<Employee> Update(Employee account, string AccountoUpdate_ID)
        {
            try
            {
                AccountData<Employee> toUpdate = Get(AccountoUpdate_ID);
                if (toUpdate == null)
                    return null;
                account._id = toUpdate.account._id;
                FilterDefinition<Employee> filter = Builders<Employee>.Filter.Eq("_id", toUpdate.account._id);
                account.Password = toUpdate.account.Password;  //copy the password again
                EmployeeCollection.FindOneAndReplace(filter, account);

                AccountData<Employee> data = new AccountData<Employee>();
                data.account = account;
                data._id = account._id.ToString();
                return data;
            }
            catch
            {
                Console.WriteLine("Inalid body response passed.");
                return null;
            }
        }
    }
}
