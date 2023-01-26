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
using CloudinaryDotNet;
using ConnectDatabase;
using MongoDB.Bson;
using MongoDB.Driver;
using Restaurant_Api.Models;

namespace Restaurant_Api.Services
{
    public class MenuServices
    {

        static ConnectDB? connection = new ConnectDB();
        static IMongoCollection<Menu>? MenuCollection = connection.Client.GetDatabase("Drum_Rock_Jerk").GetCollection<Menu>("Menu");

        public MenuServices()
        {
            /*List<BsonDocument> pipeline = new List<BsonDocument>
            {
                new BsonDocument("$lookup",
                new BsonDocument
                {
                    { "from", "menuItems" },
                    { "localField", "FoodList" },
                    { "foreignField", "_id" },
                    { "as", "menuItems" },
                }
            )
            };

            IAsyncCursor<Menu> cursor = MenuCollection.Aggregate<Menu>(pipeline);
            List<Menu> menusWithItems = cursor.ToList();*/
        }

        public static List<Menu> GetAllMenu()
        {
            return MenuCollection.Find(_ => true).ToList();
        }
        

        public static Menu Get(string id)
        {
            try
            {
                FilterDefinition<Menu> filter = Builders<Menu>.Filter.Eq("_id", id);
                Menu result = MenuCollection.Find(filter).FirstOrDefault();
                return result;
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public static void Delete(string id)
        {
            Menu toRemove = Get(id);
            if (toRemove == null)
                return;
            FilterDefinition<Menu> filter = Builders<Menu>.Filter.Eq("_id", id);
            MenuCollection.FindOneAndDelete(filter);
        }

        public static void UpdateMenu(string id, Menu newMenu)
        {
            //var index = MenuCollection.FindI(p => p.Id == id.Id);
            //if(index == -1)
            //  return;

            //object[index] = id;
            Menu toUpdate = Get(id);
            if (toUpdate == null)
                return;
            FilterDefinition<Menu> filter = Builders<Menu>.Filter.Eq("_id", toUpdate._id);
            newMenu._id = toUpdate._id;                 //Keep the old id
            MenuCollection.FindOneAndReplace(filter, newMenu);
        }

        public static Menu Add(Menu newMenu)
        {
            MenuCollection.InsertOne(newMenu);
            return newMenu;
        }
    }
}