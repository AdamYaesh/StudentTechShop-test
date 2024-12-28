using Microsoft.EntityFrameworkCore;
using StudentTechShop.API.Data;
using StudentTechShop.API.Models.Domain;
using StudentTechShop.API.Models.DTOs;

namespace StudentTechShop.API.Repositories
{
    public class SQLItemRepository : IItemRepository
    {
        private readonly AppDbContext dbContext;

        public SQLItemRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Item> CreateAsync(Item item)
        {
            item.Status = 1;
            await dbContext.Items.AddAsync(item);
            await dbContext.SaveChangesAsync();
            return item;
        }

        public async Task<Item?> DeleteAsync(Guid id)
        {
            var existingItem = await dbContext.Items.FirstOrDefaultAsync(x => x.Id == id);

            if (existingItem == null)
            {
                return null;
            }

            existingItem.Status = 2;

            dbContext.Items.Remove(existingItem);
            await dbContext.SaveChangesAsync();

            return existingItem;
        }

        public async Task<List<Item>> GetAllAsync(string? filterOn = null, string? filterQuery = null,
            string? sortBy = null, bool isAscending = true)
        {
            var items = dbContext.Items.Where(x => x.Status != 2 && x.Count > 0).ToList();

            // Filtering

            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    items = items.Where(x => x.Name.Contains(filterQuery) && x.Count > 0).ToList();
                }
            }


            // Sorting

            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    items = isAscending ? items.OrderBy(x => x.Name).ToList() : items.OrderByDescending(x => x.Name).ToList();
                }
            }

            return items;

            // Pagination

          //  var skipResults = (pageNumber - 1) * pageSize;

          //  return await items.Skip(skipResults).Take(pageSize).ToListAsync();
            //return await dbContext.users.ToListAsync();
        }

        public async Task<Item?> GetByIdAsync(Guid id)
        {
            return await dbContext.Items.Where(x => x.Status != 2 && x.Count > 0).Include(x => x.User).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Item?>> GetByNameAsync(string name)
        {

          //  return await dbContext.Items.ToListAsync(x => x.name == name);
            var items = await dbContext.Items.Where(x => x.Status != 2 && x.Count > 0).ToListAsync();

            items = items.Where(x => x.Name.ToLower().Contains(name.ToLower())).ToList();
            return  items;
        }

        public async Task<Item?> UpdateAsync(Guid id, Item item)
        {
            var existingItem = await dbContext.Items.FirstOrDefaultAsync(x => x.Id == id);

            if (existingItem == null)
            {
                return null;
            }

            existingItem.Name = item.Name;
            existingItem.Count = item.Count;
            existingItem.TimesUsed = item.TimesUsed;
            existingItem.Price = item.Price;
           // existingItem.CategoryItemId = item.CategoryItemId;
          //  existingItem.SubCategoryItemId = item.SubCategoryItemId;
            existingItem.ItemImageUrl = item.ItemImageUrl;

            await dbContext.SaveChangesAsync();
            return existingItem;
        }
    }
}
