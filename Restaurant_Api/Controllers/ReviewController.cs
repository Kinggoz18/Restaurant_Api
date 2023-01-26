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

       
   

       



        [HttpPost]
        [Route("CreateReviews")]
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
        [Route("GetReviews/{Userid}")]

        public ActionResult<List<Review>> GetReviewsByUser(string Userid )
        {
            var reviews = ReviewServices.GetReviewsByUser(Userid);
            if (reviews == null)
            {
                return NotFound();
            }
            return reviews;
        }

        [HttpDelete]
        [Route("Delete")]
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
