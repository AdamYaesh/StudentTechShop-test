namespace StudentTechShop.API.Models.Domain
{
    public class Purchase
    {
        public Guid Id { get; set; } // Unique identifier for the purchase

        public int Quantity { get; set; } // Quantity of items purchased

        public double TotalPrice { get; set; } // Total price of the purchase

        public DateTime? PurchaseDate { get; set; } // Date of the purchase

        public bool? Received { get; set; }

        public DateTime? ReceivedDate { get; set; }

        public int Status { get; set; } // Status of the purchase (e.g., completed, pending, canceled)

        public Guid ItemId { get; set; } // Foreign key to the Item table
        public Item Item { get; set; } // Navigation property for the Item

        public string UserId { get; set; } // Foreign key to the ApplicationUser table
        public ApplicationUser User { get; set; } // Navigation property for the User

        
    }
}
