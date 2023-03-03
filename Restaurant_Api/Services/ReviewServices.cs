using System;
using ConnectDatabase;
using MongoDB.Driver;
using MongoDB.Bson;
using Restaurant_Api.Models;
using System.Collections.ObjectModel;
namespace Restaurant_Api.Services
{
	public class ReviewServices
	{

        static ConnectDB connection = new ConnectDB();
        static IMongoCollection<Review> _review = connection.Client.GetDatabase("Drum_Rock_Jerk").GetCollection<Review>("Review");
        public ReviewServices()
		{
		}

        //Get All the reviews in the Database
        public static List<Review> GetAllReviews()
        {
            return _review.Find(review => true).ToList();
        }


        //create a review 
        public static void CreateReview(Review review) {

            review._id = IdGenerator.GenerateId;
            _review.InsertOne(review);

        }

        //get all reviews by id
        public static List<Review> GetReviewsByUser(string Userid)
        {
            var filter = Builders<Review>.Filter.Eq("UserId", Userid);
            var reviews = _review.Find(filter).ToList();

            return reviews;
        }

        //get customer reviews
        public static Review GetReview(string id)
        {
            var review = _review.Find<Review>(o => o._id == id).FirstOrDefault();
            return review ;
        }

        //delete customer reviews by user id 
        public static void RemoveReview(string id)
        {
            var review = _review.DeleteOne(o => o.UserId == id);
        }

        //delete customer reviews by object 
        public static void  RemoveReview(Review reviewIn)
        {
            _review.DeleteOne(order => order._id == reviewIn._id);

        }

    }
}

