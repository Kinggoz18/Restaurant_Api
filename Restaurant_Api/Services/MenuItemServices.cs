/*======================================================================
| OrderServices class
|
| Name: MenuItemServices.cs
|
| Written by: Chigozie Muonagolu - January 2023
|
| Purpose: Contains services available for a Menu Item object.
|
| usage: Used in controllers and other classes that may depend on it.
|
| Description of properties: None
|
| ------------------------------------------------------------------
*/
using System;
using ConnectDatabase;
using MongoDB.Bson;
using MongoDB.Driver;
using Restaurant_Api.Models;

namespace Restaurant_Api.Services
{
	public class MenuItemServices
    {
        static IMongoCollection<MenuItem> MenuItemCollection;   //MenuItem collection
        static ConnectDB? connection;

        public MenuItemServices()
		{
            connection = new ConnectDB();
            MenuItemCollection = connection.Client.GetDatabase("Drum_Rock_Jerk").GetCollection<MenuItem>("MenuItem");
        }
        //Get All the customers in the Database
        public static List<MenuItem> GetAll()
        {
            return MenuItemCollection.Find(_ => true).ToList();
        }
        //Get a MenuItem for a particular id
        public static MenuItem Get(ObjectId id)
        {
            FilterDefinition<MenuItem> filter = Builders<MenuItem>.Filter.Eq("_id", id);
            MenuItem result = MenuItemCollection.Find(filter).First();
            return result;
        }
        //Add MenuItem
        public static void Add(MenuItem account)
        {
            MenuItemCollection.InsertOne(account);
        }
        //Delete MenuItem
        public static void Remove(ObjectId id)
        {
            MenuItem toRemove = Get(id);
            if (toRemove == null)
                return;
            FilterDefinition<MenuItem> filter = Builders<MenuItem>.Filter.Eq("_id", id);
            MenuItemCollection.FindOneAndDelete(filter);
        }
        //Update a MenuItem
        public static void Update(MenuItem account)
        {
            MenuItem toRemove = Get(account._Id);
            if (toRemove == null)
                return;
            FilterDefinition<MenuItem> filter = Builders<MenuItem>.Filter.Eq("_id", account._Id);
            MenuItemCollection.FindOneAndReplace(filter, account);
        }
    }
}

