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

        public static Menu UpdateMenu(string id, Menu newMenu)
        {
            Menu toUpdate = Get(id);
            if (toUpdate == null)
                return null;
            FilterDefinition<Menu> filter = Builders<Menu>.Filter.Eq("_id", toUpdate._id);
            newMenu._id = toUpdate._id;                 //Keep the old id
            Menu Updated = MenuCollection.FindOneAndReplace(filter, newMenu);
            return Updated;
        }

        public static Menu Add(Menu newMenu)
        {
            newMenu._id = IdGenerator.GenerateId;
            MenuCollection.InsertOne(newMenu);
            return newMenu;
        }
    }
}