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
			string password = Environment.GetEnvironmentVariable("MONGODB_PASSWORD");
			if (password != null)
			{
                string connectionString = string.Format("mongodb+srv://billyrestaurant:{0}@cluster0.dzorkng.mongodb.net/?retryWrites=true&w=majority", password);
                client = new MongoClient(connectionString);
            }
			else
			{
				throw new Exception("MONGODB_PASSWORD Environment variable not set");
			}
        }

		public MongoClient Client
		{
			get { return this.client; }
		}
    }
}

