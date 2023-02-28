/*======================================================================
| AccountServices class
|
| Name: PromoServices.cs
|
| Written by: Chigozie Muonagolu - January 2023
|
| Purpose: Contains services available for a Promo object.
|
| usage: Used in PromoControler and other classes that may depend on it.
|
| Description of properties: None
|
|------------------------------------------------------------------
*/
using System;
using ConnectDatabase;
using MongoDB.Driver;
using Restaurant_Api.Models;

namespace Restaurant_Api.Services
{
	public class PromoServices
	{
        static ConnectDB? connection = new ConnectDB();
        static IMongoCollection<Promo>? PromoCollection = connection.Client.GetDatabase("Drum_Rock_Jerk").GetCollection<Promo>("Promos");

        public PromoServices()
		{
		}

		//Adds a promo to the database
		public static string AddPromo(Promo promoToAdd)
		{
            try
            {
                string promo = promoToAdd.promoString.ToUpper();
                if (CheckPromo(promo) == false)
                {
                    promoToAdd.promoString = promo;
                    PromoCollection.InsertOne(promoToAdd);
                    return promoToAdd.promoString;
                }
                else
                {
                    return "Promo Exists";
                }
            }
            catch {
                Console.WriteLine("Inalid body response passed.");
                return "Inavlid";
            }

        }

        //Checks if a pomo exists
        public static bool CheckPromo(string promoString)
		{
            try
            {
                string searchString = promoString.ToUpper();

                FilterDefinition<Promo> filter = Builders<Promo>.Filter.Eq("_id", searchString);
                Promo result = PromoCollection.Find(filter).FirstOrDefault();
                if (result == null)
                    return false;
                return true;
            }
            catch
            {
                Console.WriteLine("Inalid body response passed.");
                return false;
            }
        }

        //Removes a promo
        public static int RemovePromo(string promoString)
		{
            try
            {
                string promoToRemove = promoString.ToUpper();
                if (CheckPromo(promoToRemove) == false)
                {
                    return -1;
                }
                else
                {
                    FilterDefinition<Promo> filter = Builders<Promo>.Filter.Eq("_id", promoToRemove);
                    PromoCollection.FindOneAndDelete(filter);
                    return 0;
                }
            }
            catch
            {
                Console.WriteLine("Inalid body response passed.");
                return -1;
            }
        }

        //GetAll promo
        public static List<Promo> GetAllPromo(string AdminId)
        {
            try{
                Admin current = AdminServices.Get(AdminId);
                if (current == null)
                    return null;
                else
                    return PromoCollection.Find(_ => true).ToList();
            }
            catch
            {
                Console.WriteLine("Inalid body response passed.");
                return null;
            }
        }
	}
}

