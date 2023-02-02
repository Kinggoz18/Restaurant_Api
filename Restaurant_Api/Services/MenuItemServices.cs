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
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using Restaurant_Api;
using Restaurant_Api.Models;
using SharpCompress.Common;

using System.IO;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace Restaurant_Api.Services
{

    public class MenuItemServices
    {
        static Account account = new Account("dw1wmzgy1", "759371847652932", "9cBN-hEOeghmkNzuBZd5yDdnezk");
        static Cloudinary cloudinary = new Cloudinary(account);

        static ConnectDB? connection = new ConnectDB();
        static IMongoCollection<MenuItem>? MenuItemCollection = connection.Client.GetDatabase("Drum_Rock_Jerk").GetCollection<MenuItem>("MenuItem");
        static IMongoCollection<Menu>? MenuCollection = connection.Client.GetDatabase("Drum_Rock_Jerk").GetCollection<Menu>("Menu");

        MenuItemServices()
        {

        }
        //Get All the customers in the Database
        public static List<MenuItem> GetAll()
        {
            try
            {
                return MenuItemCollection.Find(_ => true).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }
        //Get a MenuItem for a particular id
        public static MenuItem Get(string Itemid)
        {
            try
            {
                FilterDefinition<MenuItem> filter = Builders<MenuItem>.Filter.Eq("_id", Itemid);
                MenuItem result = MenuItemCollection.Find(filter).FirstOrDefault();
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }
        //Add MenuItem
        public static MenuItem Add(MenuItem menuItem, IFormFile file)
        {
            try
            {
                menuItem._id = IdGenerator.GenerateId;
               
                MenuItemCollection.InsertOne(menuItem); //Add the item to the Menu Item collection
                MenuItem item = AddImage(file, menuItem.Name);  //Update its image list

                //Add the item to its menu
                FilterDefinition<Menu> filter = Builders<Menu>.Filter.Eq("Name", menuItem.Menu);
                Menu menu = MenuCollection.Find(filter).FirstOrDefault();
                menu.FoodList.Add(item);
                MenuCollection.FindOneAndReplace(filter, menu);
                return menuItem;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }
        //Delete MenuItem
        public static void Remove(string Itemid)
        {
            try
            {
                MenuItem toRemove = Get(Itemid);
                if (toRemove == null)
                    return;
                //Remove the item from cloudinary
                var PublicID = Path.Combine("MenuItem_Images", toRemove.Name);
                var deletionParams = new DeletionParams(PublicID);
                var result = cloudinary.Destroy(deletionParams);

                //Remove the item to its menu
                FilterDefinition<Menu> filter = Builders<Menu>.Filter.Eq("Name", toRemove.Menu);
                Menu menu = MenuCollection.Find(filter).FirstOrDefault();
                menu.FoodList.RemoveAll(_=>_._id == toRemove._id);
                MenuCollection.FindOneAndReplace(filter, menu);

                //Finally remove the item from the menu item collection 
                FilterDefinition<MenuItem> itemFilter = Builders<MenuItem>.Filter.Eq("_id", toRemove._id);
                MenuItemCollection.FindOneAndDelete(itemFilter);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        //Update a MenuItem
        public static MenuItem Update(string id, MenuItem newItem)
        {
            try
            {
                MenuItem toUpdate = Get(id);
                if (toUpdate == null)
                    return null;
                FilterDefinition<MenuItem> filter = Builders<MenuItem>.Filter.Eq("_id", toUpdate._id);
                newItem._id = toUpdate._id;                 //Keep the old id
                newItem.ImageLink = toUpdate.ImageLink;     //Keep the old image
                var updated = MenuItemCollection.FindOneAndReplace(filter, newItem);

                //Update the item in its menu
                FilterDefinition<Menu> menuFilter = Builders<Menu>.Filter.Eq("Name", toUpdate.Menu);
                Menu menu = MenuCollection.Find(menuFilter).FirstOrDefault();
                menu.FoodList.RemoveAll(_ => _._id == toUpdate._id);    //Remove the old one
                menu.FoodList.Add(toUpdate);  //Add it back

                MenuCollection.FindOneAndReplace(menuFilter, menu);
                return updated;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }
        //Update Item Image link
        public static MenuItem AddImage(IFormFile file, string FileName)
        {
            try
            {
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(FileName, file.OpenReadStream()),
                    PublicId = FileName,
                    Folder = "MenuItem_Images"
                };
                var uploadResult = cloudinary.Upload(uploadParams);
                //Get the Menu item object and update its link in the database
                FilterDefinition<MenuItem> filter = Builders<MenuItem>.Filter.Eq("Name", FileName);
                var update = Builders<MenuItem>.Update.Set("ImageLink", uploadResult.SecureUri);  //Change to the new url
                MenuItemCollection.UpdateOne(filter, update);
                return MenuItemCollection.Find(filter).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }
    }
}

