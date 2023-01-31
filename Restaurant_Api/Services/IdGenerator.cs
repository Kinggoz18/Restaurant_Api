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
                id = id.Insert(2, r.Next(1, 9999999).ToString()).Replace(" ", "").Replace(":", "");
                return id;
            }
        }
    }
}

