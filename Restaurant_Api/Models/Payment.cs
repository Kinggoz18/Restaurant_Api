/*======================================================================
| Payment Model class
|
| Name: Payment.cs
|
| Written by: Chigozie Muonagolu - January 2023
|
| Purpose: Blueprint for an Payment object.
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
    public interface iPayments
    {
        //The payement Id
        public ObjectId _Id { get; set; }
    }
    //Card payments
    public class CardPayment : iPayments
    {
        //The payement Id
        public ObjectId _Id { get; set; }
        //The user of payements Id
        public ObjectId ownerID { get; set; }
        //The card number 
        public int CardNumber { get; set; }
        //The expiry date of the card
        public string ExpiryDate { get; set; }
    }

    //Paypal payments
    public class PaypalPayment : iPayments
    {
        //The payement Id
        public ObjectId _Id { get; set; }
        //The users paypal email address
        public string Email { get; set; }
        //The users paypal password
        public string Password { get; set; }
    }
}

