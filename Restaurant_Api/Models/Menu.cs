using System;
using MongoDB.Bson;

namespace Restaurant_Api.Models
{
	public class Menu
	{
		public Menu()
		{
			//The Item Id
        	public ObjectId _id { get; set; }

			//check menu items to display
			public List<MenuItem>? FoodList { get; set; }

		}
	}
}

