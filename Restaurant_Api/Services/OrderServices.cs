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

        public static void UpdateOrder(string orderId, Order orderIn)
        {
            
        //    var filter = Builders<Order>.Filter.Eq("_id", orderId);
        //    var update = Builders<Order>.Update;

        //    //loop through the items list and find the item you want to update
        //    foreach (var item in orderIn.items)
        //    {
        //        // Check if the item has an id and if it matches the id of the item you want to update
        //        if (item._id != null && item._id == orderIn._id)
        //        {
        //            // Update the item name
        //            update = update.Set("Items.$[i].Name", item.Name);
        //            break;
        //        }
        //    }
        //    update = null;

        //   update  = update.Set("Items", orderIn.items)
        //                                        .Set("Customer", orderIn.CustomerName)
        //                                        .Set("Status", orderIn.status)
        //                                        .Set("TotalPrice", orderIn.TotalPrice);
        //    // Update the order
        //    var options = new FindOneAndUpdateOptions<Order> { ArrayFilters = new
        //        List<ArrayFilterDefinition> { new
        //        BsonDocumentArrayFilterDefinition<BsonDocument>(new BsonDocument("i._id", orderIn._id)
        //    } };
        //    _orders.FindOneAndUpdate(filter, update, options);
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

    






    


