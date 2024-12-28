using StudentTechShop.API.Models.Domain;

namespace StudentTechShop.API.Repositories
{
    public interface IProjectRepository
    {
        Task<List<Project>> GetAllAsync(string? filterOn = null, string? filterQuery = null,
            string? sortBy = null, bool isAscending = true);
        Task<Project?> GetByIdAsync(Guid id);
        Task<List<Project?>> GetByNameAsync(string name);
        Task<Project> CreateAsync(Project project);
        Task<Project?> UpdateAsync(Guid id, Project project);
        //Task<Project?> DeleteAsync(Guid id);
    }
}
