/*======================================================================
| MenuItem Model class
|
| Name: MenuItem.cs
|
| Written by: Chigozie Muonagolu - January 2023
|
| Purpose: Blueprint for an MenuItem object.
|
| usage: Models for database schema.
|
| Description of properties:
| argv[1] - number if random number pairs to generate
|
|------------------------------------------------------------------
*/
using System;
using MongoDB.Bson;

namespace Restaurant_Api.Models
{
	public class MenuItem
	{
        //The Item Id
        public ObjectId _id { get; set; }
        //Name of the item
        public string? Name { get; set; }
        //Price of the item
        public double? Price { get; set; }
        //Menu the item belongs to
        public string? Menu { get; set; }
        //Number of time Item has been ordered
        public int? OrderCount { get; set; }
        //Menu Item image name
        public string? ImageLink { get; set; }
    }
}

