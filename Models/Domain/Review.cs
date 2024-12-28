namespace StudentTechShop.API.Models.Domain
{
    public class Review
    {
        public Guid Id { get; set; }

        public string Comment { get; set; }

        public int Grade { get; set; }

        public string? Description { get; set; }

        public int Status { get; set; }

        public Guid ItemId { get; set; }
        public Item Item { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }



    }
}
