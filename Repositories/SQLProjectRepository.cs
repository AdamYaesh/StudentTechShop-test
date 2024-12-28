using Microsoft.EntityFrameworkCore;
using StudentTechShop.API.Data;
using StudentTechShop.API.Models.Domain;

namespace StudentTechShop.API.Repositories
{
    public class SQLProjectRepository : IProjectRepository
    {
        private readonly AppDbContext dbContext;

        public SQLProjectRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Project> CreateAsync(Project project)
        {
            project.Status = 1;
            await dbContext.Projects.AddAsync(project);
            await dbContext.SaveChangesAsync();
            return project;
        }

        /*public async Task<Project?> DeleteAsync(Guid id)
        {
            var existingProject = await dbContext.Projects.FirstOrDefaultAsync(x => x.Id == id);

            if (existingProject == null)
            {
                return null;
            }

            existingProject.Status = 2;

            dbContext.Projects.Remove(existingProject);
            await dbContext.SaveChangesAsync();

            return existingProject;
        }*/

        public async Task<List<Project>> GetAllAsync(string? filterOn = null, string? filterQuery = null,
            string? sortBy = null, bool isAscending = true)
        {
            var projects = dbContext.Projects.Where(x => x.Status != 2).ToList();

            // Filtering

            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    projects = projects.Where(x => x.Name.Contains(filterQuery)).ToList();
                }
            }


            // Sorting

            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    projects = isAscending ? projects.OrderBy(x => x.Name).ToList() : projects.OrderByDescending(x => x.Name).ToList();
                }
            }

            return projects;

            // Pagination

            //var skipResults = (pageNumber - 1) * pageSize;

            // return await projects.Skip(skipResults).Take(pageSize);
            //return await dbContext.users.ToListAsync();
        }

        public async Task<Project?> GetByIdAsync(Guid id)
        {
            return await dbContext.Projects.Include(x => x.Items).Include(x => x.User).Where(x => x.Status != 2).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Project?>> GetByNameAsync(string name)
        {

            //  return await dbContext.Items.ToListAsync(x => x.name == name);
            var projects = await dbContext.Projects.Where(x => x.Status != 2).ToListAsync();

            projects = projects.Where(x => x.Name.ToLower().Contains(name.ToLower())).ToList();
            return projects;
        }

        public async Task<Project?> UpdateAsync(Guid id, Project project)
        {
            var existingProject = await dbContext.Projects.FirstOrDefaultAsync(x => x.Id == id);

            if (existingProject == null)
            {
                return null;
            }

            existingProject.Name = project.Name;
            existingProject.Cost = project.Cost;
            existingProject.Description = project.Description;
            existingProject.ProjectImageUrl = project.ProjectImageUrl;

            await dbContext.SaveChangesAsync();
            return existingProject;
        }
    }
}
