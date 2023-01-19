/*======================================================================
| Menu class
|
| Name: Menu.cs
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
using ConnectDatabase;
using MongoDB.Bson;
using MongoDB.Driver;
using Restaurant_Api.Models;

namespace Restaurant_Api.Services
{
    public class MenuServices
    {
        static IMongoCollection<Menu>? MenuCollection;   //Menu collection
        static ConnectDB? connection;

        public MenuServices()
        {
            connection = new ConnectDB();
            MenuCollection = connection.Client.GetDatabase("Drum_Rock_Jerk").GetCollection<Menu>("Menu");

        }

        public static List<Menu> GetAllMenu()
        {
            return MenuCollection.Find(_ => true).ToList();
        }

        public static Menu Get(object id)
        {
            FilterDefinition<Menu> filter = Builders<Menu>.Filter.Eq("_id", id);
            Menu result = MenuCollection.Find(filter).First();
            return result;
        }

        public static void Delete(object id)
        {
            Menu toRemove = Get(id);
            if (toRemove == null)
                return;
            FilterDefinition<Menu> filter = Builders<Menu>.Filter.Eq("_id", id);
            MenuCollection.FindOneAndDelete(filter);
        }

        public static void updateMenu(Menu account)
        {
            //var index = MenuCollection.FindI(p => p.Id == id.Id);
            //if(index == -1)
            //  return;

            //object[index] = id;
            Menu toRemove = Get(account._id);
            if (toRemove == null)
                return;
            FilterDefinition<Menu> filter = Builders<Menu>.Filter.Eq("_id", account._id);
            MenuCollection.FindOneAndReplace(filter, account);


        }
        public static void Add(Menu account)
        {
            MenuCollection.InsertOne(account);
        }
    }
}