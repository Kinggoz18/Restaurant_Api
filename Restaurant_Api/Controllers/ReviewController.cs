using System;
using MongoDB.Bson;
using Restaurant_Api.Services;
using Restaurant_Api.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace Restaurant_Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReviewsController : ControllerBase
    {

        public ReviewsController()
        {

        }

        [HttpGet]
        public static  List<Review> Get()
        {
            return ReviewServices.GetallReviews();
        }

        [HttpGet("{id}")]
        public ActionResult<Review> Get(string id)
        {
            var review = ReviewServices.GetReview(ObjectId.Parse(id));

            if (review == null)
            {
                return NotFound();
            }

            return review;
        }



        [HttpPost]
        public void Create(Review review)
        {
            ReviewServices.CreateReview(review);
        }

        

        [HttpGet]
        [Route("GetAllReviews")]
        public ActionResult<List<Review>> GetAllReviews()
        {
            var reviews = ReviewServices.GetAllReviews();
            if (reviews == null)
            {
                return NotFound();
            }
            return reviews;
        }

        // GET api/reviews/username
        [HttpGet]
        [Route("GetReviews/{id}")]

        public ActionResult<List<Review>> GetReviewsByUser(string userName)
        {
            var reviews = ReviewServices.GetReviewsByUser(userName);
            if (reviews == null)
            {
                return NotFound();
            }
            return reviews;
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var review = ReviewServices.GetReview(ObjectId.Parse(id));

            if (review == null)
            {
                return NotFound();
            }

            ReviewServices.RemoveReview(ObjectId.Parse(id));
            return NoContent();
        }
    }
}
