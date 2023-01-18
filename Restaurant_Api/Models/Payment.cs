/*======================================================================
| Payment Model class
|
| Name: Payment.cs
|
| Written by: Chigozie Muonagolu - January 2023
|
| Purpose: Blueprint for an Payment object.
|
| usage: Models for database schema.
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
    //Card payments
    public class CardPayment
    {
        //The payement Id
        public ObjectId _id { get; set; }
        //The user of payements Id
        public ObjectId OwnerID { get; set; }
        //The card number 
        public int CardNumber { get; set; }
        //The expiry date of the card
        public string? ExpiryDate { get; set; }
    }

    //Paypal payments
    public class PaypalPayment
    {
        //The payement Id
        public ObjectId _id { get; set; }
        //The users paypal email address
        public string? Email { get; set; }
    }

    //Card credentials - Would not be stored in the database
    public class CardDetails
    {
        //The user of payements Id
        public ObjectId OwnerID { get; set; }
        //The users first name
        public string? FirstName { get; set; }
        //The users last name
        public string? LastName { get; set; }
        //The card Number
        public int CardNumber { get; set; }
        //The expiry date of the card
        public string? ExpiryDate { get; set; }
        //The card Number
        public int CVV { get; set; }
        //The users postal code Number
        public string? PostalCode { get; set; }

    }
}

