/*======================================================================
| OrderServices class
|
| Name: OrderServices.cs
|
| Written by: Tobi AA- January 2023
|
| Purpose: Contains services available for a Order object.
|
| usage: Used in PaymentControler and other classes that may depend on it.
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
using ConnectDatabase;
using System.Collections.ObjectModel;

namespace Restaurant_Api.Services
{
	public class OrderServices
	{
        static ConnectDB connection = new ConnectDB();
        static IMongoCollection<Order> _orders = connection.Client.GetDatabase("Drum_Rock_Jerk").GetCollection<Order>("Orders");
        public OrderServices(IMongoDatabase database) 
		{

        }
        // provides a list of orders
        public static List<Order> GetallOrder()
        {
            return _orders.Find(order => true).ToList();
        }
        //get customers orders
        public static Order GetOrder(ObjectId id)
        {
            var order = _orders.Find<Order>(o => o._id == id).FirstOrDefault();
            return order;
        }
        //create an order 

        public static void CreateOrder(Order order)
        {
            _orders.InsertOne(order);
        }

        public static void UpdateOrder( Order orderIn, string orderId)
        {
            var filter = Builders<Order>.Filter.Eq("_id", orderId);
            var update = Builders<Order>.Update.Set("Items", orderIn.items)
                                                .Set("Customer", orderIn.CustomerName)
                                                .Set("Status", orderIn.status)
                                                .Set("TotalPrice", orderIn.TotalPrice);
            _orders.UpdateOne(filter, update);
        }

        public static void RemoveOrder(Order orderIn)
        {
            _orders.DeleteOne(order => order._id == orderIn._id);
        }
        //delete a single document from a collection that matches a specified filter. In

        public static void RemoveOrder(ObjectId id)
        {
            _orders.DeleteOne(order => order._id == id);
        }
    }
    






    
}

