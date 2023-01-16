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

namespace Restaurant_Api.Services
{
	public class OrderServices
	{
        private readonly IMongoCollection<Order> _orders;
        public OrderServices(IMongoDatabase database) 
		{
            _orders = database.GetCollection<Order>("orders");

        }
        // provides a list of orders
        public List<Order> GetallOrder()
        {
            return _orders.Find(order => true).ToList();
        }
        //get customers orders
        public Order GetOrder(ObjectId id)
        {
            var order = _orders.Find<Order>(o => o._Id == id).FirstOrDefault();
            return order;
        }
        //create an order 

        public Order CreateOrder(Order order)
        {
            _orders.InsertOne(order);
            return order;
        }

        public void UpdateOrder(ObjectId id, Order orderIn)
        {
            _orders.ReplaceOne(order => order._Id == id, orderIn);
        }

        public void RemoveOrder(Order orderIn)
        {
            _orders.DeleteOne(order => order._Id == orderIn._Id);
        }
        //delete a single document from a collection that matches a specified filter. In

        public void RemoveOrder(ObjectId id)
        {
            _orders.DeleteOne(order => order._Id == id);
        }
    }
    






    
}

