namespace StudentTechShop.API.Models.DTOs
{
    public class PurchaseDto
    {
        public int Quantity { get; set; } // Quantity of items purchased
       // public double TotalPrice { get; set; } // Total price of the purchase

      //  public DateTime? PurchaseDate { get; set; } // Date of the purchase

        public Guid ItemId { get; set; }
      //  public ItemPurchase Item { get; set; }

        public string UserId { get; set; }
      //  public UserView User { get; set; }

    }

    public class PurchaseResponseDto
    {
        public int Quantity { get; set; } // Quantity of items purchased
        public double TotalPrice { get; set; } // Total price of the purchase
        public DateTime? PurchaseDate { get; set; } // Date of the purchase

        public Guid ItemId { get; set; }
        //  public ItemPurchase Item { get; set; }

        public string UserId { get; set; }
        //  public UserView User { get; set; }
    }



    public class PurchaseView
    {
        public int Quantity { get; set; } // Quantity of items purchased
        public double TotalPrice { get; set; } // Total price of the purchase
        public DateTime? PurchaseDate { get; set; } // Date of the purchase

        public bool? Received { get; set; }
        public DateTime? ReceivedDate { get; set; }


        public Guid ItemId { get; set; }
        public ItemPurchase Item { get; set; }

        public string UserId { get; set; }
        public UserView User { get; set; }
    }


    public class UpdatePurchase
    {
        public int Quantity { get; set; } // Quantity of items purchased
        public double TotalPrice { get; set; } // Total price of the purchase
    }
}
