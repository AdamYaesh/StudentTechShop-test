namespace StudentTechShop.API.Models.Domain
{
    public class Item
    {
        public Guid Id { get; set; }

        public string Name { get; set; } 
        
        public string? Description { get; set; }

        public double Price { get; set; }

        public int TimesUsed { get; set; }

        public int Count { get; set; }

        public DateTime Date { get; set; }

        public string? ItemImageUrl { get; set; }

        public int Status { get; set; }


        public Guid? CategoryItemId { get; set; } // مفتاح خارجي
        public CategoryItem CategoryItem { get; set; }


        public Guid? SubCategoryItemId { get; set; } // مفتاح خارجي
        public SubCategoryItem SubCategoryItem { get; set; }


        public Guid? ProjectId { get; set; }
        public Project Project { get; set; }


        public List<Review> Reviews { get; set; }

        public List<Purchase> Purchases { get; set; } // Buyers via purchases


        public string UserId { get; set; } // مفتاح خارجي
        public ApplicationUser User { get; set; } // كائن البائع
    }
}


