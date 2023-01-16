/*======================================================================
| ConnectDB class
|
| Name: ConnectDB.cs
|
| Written by: Chigozie Muonagolu - January 2023
|
| Purpose: Creating a connection to the database.
|
| usage: Called in services to facilitate database connection.
|
| Description of properties: None
|
|------------------------------------------------------------------
*/
using System;
using MongoDB.Driver;
using MongoDB.Bson;
namespace ConnectDatabase
{
	public class ConnectDB
	{
		private readonly MongoClient client;
        public ConnectDB()
		{

			client = new MongoClient("mongodb+srv://billyrestaurant:{Restaurant2022}@cluster0.dzorkng.mongodb.net/?retryWrites=true&w=majority");
        }

		public MongoClient Client
		{
			get { return this.client; }
		}
    }
}

