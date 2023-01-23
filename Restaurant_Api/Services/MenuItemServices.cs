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
                ObjectId id = new ObjectId(Itemid);
                FilterDefinition<MenuItem> filter = Builders<MenuItem>.Filter.Eq("_id", id);
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
        public static MenuItem Add(MenuItem menuItem)
        {
            try
            {
                MenuItemCollection.InsertOne(menuItem);
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

                FilterDefinition<MenuItem> filter = Builders<MenuItem>.Filter.Eq("_id", toRemove._id);
                MenuItemCollection.FindOneAndDelete(filter);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        //Update a MenuItem
        public static void Update(string id, MenuItem newItem)
        {
            try
            {
                MenuItem toUpdate = Get(id);
                if (toUpdate == null)
                    return;
                FilterDefinition<MenuItem> filter = Builders<MenuItem>.Filter.Eq("_id", toUpdate._id);
                newItem._id = toUpdate._id;                 //Keep the old id
                newItem.ImageLink = toUpdate.ImageLink;     //Keep the old image
                MenuItemCollection.FindOneAndReplace(filter, newItem);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        //Update Item Image link
        public static async void AddImage(IFormFile file, string FileName)
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
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}

