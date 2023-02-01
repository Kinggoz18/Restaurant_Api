using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Restaurant_Api.Models
{
	public class Menu
	{

        //The Item Id
        [BsonRepresentation(BsonType.String)]
        public string? _id { get; set; }

        //check menu items to display
        public List<MenuItem>? FoodList { get; set; }

    }
}


