using StudentTechShop.API.Models.Domain;

namespace StudentTechShop.API.Repositories
{
    public interface IImageRepository
    {
        Task<Image> Upload(Image image);
    }
}
