/*======================================================================
| Menu class
|
| Name: SalesServices.cs
|
| Written by: Williams Agbo - January 2023
|
| Purpose: Contains services available for a Menu object.
|
| usage: Used in controllers and other classes that may depend on it.
|
| Description of properties: None
|
| ------------------------------------------------------------------
*/
using System;
using CloudinaryDotNet;
using ConnectDatabase;
using MongoDB.Bson;
using MongoDB.Driver;
using Restaurant_Api.Models;

namespace Restaurant_Api.Services
{
    public class SalesServices{
        static ConnectDB? connection = new ConnectDB();
        static IMongoCollection<Sales>? SalesCollection = connection.Client.GetDatabase("Drum_Rock_Jerk").GetCollection<Sales>("Sales");
         static IMongoCollection<Order>? OrderCollection = connection.Client.GetDatabase("Drum_Rock_Jerk").GetCollection<Sales>("Orde");

        public SalesServices()
        {

        }
        
        public double GetTotalSales()
        {
            var orders = OrderCollection.Find(new BsonDocument()).ToList();
            double totalSales = 0;
            foreach (var Order in Order)
            {
                totalSales += Order.TotalPrice;
            }
            return totalSales;
        }
    }


}