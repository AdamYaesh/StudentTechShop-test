using StudentTechShop.API.Models.Domain;

namespace StudentTechShop.API.Repositories
{
    public interface IItemRepository
    {
        Task<List<Item>> GetAllAsync(string? filterOn = null, string? filterQuery = null,
            string? sortBy = null, bool isAscending = true);
        Task<Item?> GetByIdAsync(Guid id);
        Task<List<Item?>> GetByNameAsync(string name);
        Task<Item> CreateAsync(Item item);
        Task<Item?> UpdateAsync(Guid id, Item item);
        Task<Item?> DeleteAsync(Guid id);
    }
}
