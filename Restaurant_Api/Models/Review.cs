/*======================================================================
| Order Model class
|
| Name: Review.cs
|
| Written by: Tobi Akinnola - January 2023
|
| Purpose: Blueprint for the  reviewobject.
|
| usage: None.
|

| Description of propertiesId: is a unique identifier for the order in MongoDB. It is stored as an ObjectId and is used as the primary key for the order document in the MongoDB collection.
|   CustomerName: is the name of the customer who made the order.
|   OrderItems: is a list of OrderItem objects that contain the details of each item in the order.
|   TotalPrice: is the total price for the order, which is calculated by summing up the prices of all the items in the order.
|   OrderDate: is the date and time the order was made.
| 
|
|------------------------------------------------------------------
*/

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class Review
{

    [BsonRepresentation(BsonType.String)]
    public string? _id { get; set; }
    public string? Description { get; set; }
    public int Rating { get; set; }
    public string? Name { get; set; }
    public string? UserId { get; set; }
}
