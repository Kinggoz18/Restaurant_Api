/*======================================================================
| Order Model class
|
| Name: Order.cs
|
| Written by: Tobi Akinnola - January 2023
|
| Purpose: Blueprint for the order object.
|
| usage: None.
|

| Description of propertiesId: is a unique identifier for the order in MongoDB. It is stored as an ObjectId and is used as the primary key for the order document in the MongoDB collection.
|   CustomerName: is the name of the customer who made the order.
|   OrderItems: is a list of OrderItem objects that contain the details of each item in the order.
|   TotalPrice: is the total price for the order, which is calculated by summing up the prices of all the items in the order.
|   OrderDate: is the date and time the order was made.
| 
|
|------------------------------------------------------------------
*/



using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
namespace Restaurant_Api.Models
{
    public class Order
    {
        //this is the customer id 
        public ObjectId _Id { get; set; }
        //the users phone number 
        public string? PhoneNumber { get; set; }
        // the users order item gotten form the menu 
        public List<MenuItem>? items { get; set; }
        //the date and time the user ordered 
        public DateTime orderdate { get; set; }
        //the total price for the order
        public decimal TotalPrice { get; set; }
        





    }
}

