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
    public class Menu
    {
        static IMongoCollection<Menu>? Menu;   //Menu collection
        static ConnectDB? connection;

        public Menu(){

        }

        public static List<Menu> GetAllMenu(){
            return Menu.Find(_ => true).ToList();
        }

        public static void GetMenu(object id){
            FilterDefinition<Menu> filter = Builders<Menu>.Filter.Eq("_id", id);
            Menu result = MenuCollection.Find(filter).First();
            return result; 
        }

        public static void Delete(object id){
            Menu toRemove = Get(id);
            if (toRemove == null)
                return;
            FilterDefinition<Menu> filter = Builders<Menu>.Filter.Eq("_id", id);
            MenuCollection.FindOneAndDelete(filter);
        }

        public static void updateMenu(object id){
            var index = object.FindIndex(p => p.Id == id.Id);
            if(index == -1)
                return;

            object[index] = id;
        }

        Public static void addMenu(Menu account){
             MenuCollection.InsertOne(account);
        }

    }
}