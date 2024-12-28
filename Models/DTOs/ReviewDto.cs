namespace StudentTechShop.API.Models.DTOs
{
    public class ReviewDto
    {
        public string Comment { get; set; }

        public int Grade { get; set; }

    }

    public class AddReview
    {
        public string Comment { get; set; }

        public int Grade { get; set; }

        public Guid ItemId { get; set; }

        public string UserId { get; set; }

    }

    public class ReviewView
    {
        public string Comment { get; set; }

        public int Grade { get; set; }

        public UserView User { get; set; } // Add the user info here
    }
}
