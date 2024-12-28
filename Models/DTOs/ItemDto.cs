using Microsoft.Extensions.Hosting;
using StudentTechShop.API.Models.Domain;

namespace StudentTechShop.API.Models.DTOs
{
    public class ItemDto
    {
      //  public Guid Id { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

        public double Price { get; set; }

        public int TimesUsed { get; set; }

        public int Count { get; set; }

        public string UserId { get; set; } // مفتاح خارجي

        public Guid? CategoryItemId { get; set; }

        public Guid? SubCategoryItemId { get; set; }

        public string? ItemImageUrl { get; set; }

        public UserView User { get; set; } // كائن الناشر


    }


    public class ItemView
    {
        public string Name { get; set; }

        public string? Description { get; set; }

        public double Price { get; set; }

        public int TimesUsed { get; set; }

        public int Count { get; set; }

        public string UserId { get; set; } // مفتاح خارجي

        public string? ItemImageUrl { get; set; }

        public ItemView() { }

        public ItemView(Item item)
        {
            Name = item.Name;
            Price = item.Price;
            Description = item.Description;
            ItemImageUrl = item.ItemImageUrl;
            TimesUsed = item.TimesUsed;
            Count = item.Count;
        }

    }

    public class ItemPurchase
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public int Count { get; set; }
        public string? ItemImageUrl { get; set; }

    }


}
