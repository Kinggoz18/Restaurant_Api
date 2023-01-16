﻿/*======================================================================
| MenuItem Model class
|
| Name: MenuItem.cs
|
| Written by: Chigozie Muonagolu - January 2023
|
| Purpose: Blueprint for an MenuItem object.
|
| usage: None.
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
        public ObjectId _Id { get; set; }
        //Name of the item
        public string? Name { get; set; }
        //Price of the item
        public BsonDouble? Price { get; set; }
        //Menu the item belongs to
        public string? Menu { get; set; }
    }
}

