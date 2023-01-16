/*======================================================================
| Account Model class
|
| Name: Account.cs
|
| Written by: Chigozie Muonagolu - January 2023
|
| Purpose: Blueprint for an account object.
|
| usage: None.
|

| Description of properties:
| argv[1] - number if random number pairs to generate
|
|------------------------------------------------------------------
*/

using System;
using MongoDB.Bson;
namespace Restaurant_Api.Models
{
    public interface iAccount
    {
        //The user ID
        public ObjectId _Id { get; set; }
    }
	public class Customer : iAccount
	{
		//The user ID
        public ObjectId _Id { get; set; }
        //The user first name
        public string? FirstName { get; set; }
        //The user last name
        public string? LastName { get; set; }
        //The user phone number
        public string? PhoneNumber { get; set; }
        //The user email address
        public string? EmailAddress { get; set; }
        //The user password
        public string? Password { get; set; }
        //The user points accumulated
        public int Points { get; set; }
        //A list of the users payments
        public List<iPayments>? payments { get; set; }
        //A list of the users past orders
        public List<Order>? PastOrders { get; set; }

    }
    public class Admin : iAccount
    {
        //The user ID
        public ObjectId _Id { get; set; }
        //The user first name
        public string? FirstName { get; set; }
        //The user last name
        public string? LastName { get; set; }
        //The user phone number
        public string? PhoneNumber { get; set; }
        //The user email address
        public string? EmailAddress { get; set; }
        //The user password
        public string? Password { get; set; }
    }
    public class Employee : iAccount
    {
        //The user ID
        public ObjectId _Id { get; set; }
        //The user first name
        public string? FirstName { get; set; }
        //The user last name
        public string? LastName { get; set; }
        //The user phone number
        public string? PhoneNumber { get; set; }
        //The user email address
        public string? EmailAddress { get; set; }
        //The user password
        public string? Password { get; set; }
    }
}

