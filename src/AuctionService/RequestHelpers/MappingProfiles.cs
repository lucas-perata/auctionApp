using AuctionService.DTOs;
using AuctionService.Entities;
using AutoMapper;
using Contracts;

namespace AuctionService.RequestHelpers
{
    public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Auction, AuctionDto>()
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Item.Title))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Item.Description))
            .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.Item.ImageUrl))
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Item.Category.Name));
        CreateMap<CreateAuctionDto, Auction>()
            .ForMember(d => d.Item, o => o.MapFrom(s => s)); 
        CreateMap<CreateAuctionDto, Item>()
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl)); 
        CreateMap<AuctionDto, AuctionCreated>(); 
        CreateMap<Auction, AuctionCreated>()
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Item.Title))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Item.Description))
            .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.Item.ImageUrl))
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Item.Category.Name));
        CreateMap<Category, AuctionUpdated>(); 
        CreateMap<Auction, AuctionUpdated>().IncludeMembers(a => a.Item).IncludeMembers(a => a.Item.Category);
        CreateMap<Item, AuctionUpdated>(); 
        
    }
}
}
