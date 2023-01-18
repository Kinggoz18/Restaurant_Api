/*======================================================================
| PaymentServices class
|
| Name: PaymentServices.cs
|
| Written by: Chigozie Muonagolu - January 2023
|
| Purpose: Contains services available for a Payment object.
|
| usage: Used in PaymentControler and other classes that may depend on it.
|
| Description of properties: None
|
|------------------------------------------------------------------
*/
using System;
using ConnectDatabase;
using MongoDB.Bson;
using MongoDB.Driver;
using Restaurant_Api.Models;

namespace Restaurant_Api.Services
{
	//Add payment
	//Remove Payment
	//Get Payment
	public interface iPaymentServices<T>
	{
		public static abstract void Add(T payment);
		public static abstract void Remove(ObjectId id);
		public static abstract T Get(ObjectId id);
	}

	//Make payment
	//Refund payment
    public class CardPaymentServices : iPaymentServices<CardPayment>
    {
        static IMongoCollection<CardPayment>? CardCollection;
        ConnectDB? connection;
        //Default constructor
        public CardPaymentServices()
		{
            connection = new ConnectDB();
            CardCollection = connection.Client.GetDatabase("Drum_Rock_Jerk").GetCollection<CardPayment>("Card_Payments");
        }

        //Adds a new card
        public static void Add(CardPayment payment)
        {
            CardCollection.InsertOne(payment);
        }

        //Returns a card details
        public static CardPayment Get(ObjectId id)
        {
            FilterDefinition<CardPayment> filter = Builders<CardPayment>.Filter.Eq("_id", id);
            CardPayment result = CardCollection.Find(filter).First();
            return result;
        }

        //Removes a card
        public static void Remove(ObjectId id)
        {
            CardPayment toRemove = Get(id);
            if (toRemove == null)
                return;
            FilterDefinition<CardPayment> filter = Builders<CardPayment>.Filter.Eq("_id", id);
            CardCollection.FindOneAndDelete(filter);
        }
        //Makes a refund to a particular card
        public static void RefundCard(ObjectId id)
        {
            throw new NotImplementedException();
        }
        //Makes a payment using the customers card
        public static void MakePayment()
        {
            throw new NotImplementedException();
        }
    }

    //Make payment
    //Refund payment
    public class PaypalPaymentServices : iPaymentServices<PaypalPayment>
    {
        static IMongoCollection<PaypalPayment>? PayPalCollection;
        ConnectDB? connection;
        //Default constructor
        public PaypalPaymentServices()
        {
            connection = new ConnectDB();
            PayPalCollection = connection.Client.GetDatabase("Drum_Rock_Jerk").GetCollection<PaypalPayment>("Card_Payments");
        }

        //Adds a new paypal account
        public static void Add(PaypalPayment payment)
        {
            //Implement validation here
            PayPalCollection.InsertOne(payment);
        }

        //Returns a paypal account detail
        public static PaypalPayment Get(ObjectId id)
        {
            FilterDefinition<PaypalPayment> filter = Builders<PaypalPayment>.Filter.Eq("_id", id);
            PaypalPayment result = PayPalCollection.Find(filter).First();
            return result;
        }

        //Removes a paypal account
        public static void Remove(ObjectId id)
        {
            PaypalPayment toRemove = Get(id);
            if (toRemove == null)
                return;
            FilterDefinition<PaypalPayment> filter = Builders<PaypalPayment>.Filter.Eq("_id", id);
            PayPalCollection.FindOneAndDelete(filter);
        }
        //Makes a refund to a particular paypal account
        public static void RefundCard(ObjectId id)
        {
            throw new NotImplementedException();
        }
        //Makes a payment using the customers paypal account
        public static void MakePayment()
        {
            throw new NotImplementedException();
        }
    }
}

