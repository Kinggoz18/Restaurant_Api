/*======================================================================
| AccountServices class
|
| Name: AccountServices.cs
|
| Written by: Chigozie Muonagolu - January 2023
|
| Purpose: Generates custom IDs for the objects.
|
| usage: Used in the services and other classes that may depend on it.
|
| Description of properties: GenerateId: Generates a id using the currrent timestap, and random integers
|
|------------------------------------------------------------------
*/
using System;
namespace Restaurant_Api.Services
{
    public class IdGenerator
    {
        public IdGenerator() { }

        //Property to return Id
        public static string GenerateId
        {
            get
            {
                string id = "";
                id += DateTime.Now.ToString();
                Random r = new Random();
                id = id.Insert(1, r.Next(1, 9999).ToString())
                    .Insert(2, r.Next(1, 99999).ToString())
                    .Insert(5, r.Next(1, 999).ToString())
                    .Replace(" ", "").Replace(":", "").Replace("/", "").Replace("-", "");
                return id;
            }
        }
    }
}

