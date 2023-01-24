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
        public ReviewServices(IMongoDatabase database)
		{
		}

        //Get All the reviews in the Database
        public static List<Review> GetallReviews()
        {
            return _review.Find(review => true).ToList();
        }


        //create a review 
        public static void CreateReview(Review review) {

            _review.InsertOne(review);

        }

        ////returns all the review
        public static List<Review> GetAllReviews()
        {
            var filter = Builders<Review>.Filter.Empty;
            var review = _review.Find(filter).ToList();
            return review;
        }

        //get all reviews by id
        public static List<Review> GetReviewsByUser(string userName){
            var filter = Builders<Review>.Filter.Eq("CustomerName", userName);
            var reviews = _review.Find(filter).ToList();

            return reviews;
        }

        //get customer reviews
        public static Review GetReview(ObjectId id)
        {
            var review = _review.Find<Review>(o => o._id == id).FirstOrDefault();
            return review ;
        }

        //delete customer reviews by object id 
        public static Review RemoveReview(ObjectId id)
        {
            var review = _review.Find<Review>(o => o._id == id).FirstOrDefault();
            return review;
        }

        //delete customer reviews by object 
        public static void  RemoveReview(Review reviewIn)
        {
            _review.DeleteOne(order => order._id == reviewIn._id);

        }

    }
}

