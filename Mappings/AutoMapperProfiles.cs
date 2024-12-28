using AutoMapper;
using StudentTechShop.API.Models.Domain;
using StudentTechShop.API.Models.DTOs;

namespace StudentTechShop.API.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Item, ItemDto>().ReverseMap();
            CreateMap<AddItemRequestDto, Item>().ReverseMap();
            CreateMap<UpdateItemRequestDto, Item>().ReverseMap();
            CreateMap<Item, ItemView>().ReverseMap();
            CreateMap<Item, ItemPurchase>().ReverseMap();


            CreateMap<Project, ProjectDto>().ReverseMap();
            CreateMap<AddProjectRequestDto, Project>().ReverseMap();
            CreateMap<UpdateProjectRequestDto, Project>().ReverseMap();
            CreateMap<Project, ProjectView>().ReverseMap();



            CreateMap<ApplicationUser, UserDto>().ReverseMap();
            CreateMap<ApplicationUser, UserView>().ReverseMap();
          //  CreateMap<ApplicationUser, UpdateUserRequestDto>().ReverseMap();
          //  CreateMap<ApplicationUser, RegisterRequestDto>().ReverseMap();
          //  CreateMap<ApplicationUser, LoginRequestDto>().ReverseMap();
          //  CreateMap<ApplicationUser, LoginResponseDto>().ReverseMap();




            CreateMap<Review, ReviewDto>().ReverseMap();
            CreateMap<Review, ReviewView>().ReverseMap();
            CreateMap<Review, AddReview>().ReverseMap();


            CreateMap<Purchase, PurchaseDto>().ReverseMap();
            //CreateMap<List<Purchase>, List<PurchaseView>>();
            CreateMap<Purchase, PurchaseView>().ReverseMap();
            CreateMap<Purchase, UpdatePurchase>().ReverseMap();


        }
    }
}
