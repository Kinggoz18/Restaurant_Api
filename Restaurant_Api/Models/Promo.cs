using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Restaurant_Api.Models
{
	public class Promo
	{
        //The promo string is its ID
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        [BsonElement("Id")]
        public string? promoString { get; set; }

        public int? promoDiscount { get; set; }
    }
}

