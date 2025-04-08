using AutoMapper;
using Dal.Models;
using DTO.Classes;


namespace BLL
{
    public class MarketProfile:Profile
    {
        public MarketProfile()
        {
            CreateMap<Order, OrderDto>()
                .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.OrderState!.StateDescription))
                .ForMember(dest=>dest.Prods,opt=>opt.MapFrom(src=>src.Prods));
            CreateMap<OrderDto, Order>()
                .ForMember(dest => dest.OrderStateId, opt => opt.MapFrom(src=>src.OrderStateId))
                .ForMember(dest => dest.OrderState, opt => opt.Ignore())
                .ForMember(dest => dest.Prods, opt => opt.MapFrom(src => src.Prods))
                .ForMember(dest => dest.Supplier, opt => opt.Ignore());

            CreateMap<State, StateDto>().ReverseMap();

            CreateMap<StockDto, Stock>()
                .ForMember(dest => dest.Supplier, opt => opt.Ignore())
                .ForMember(dest => dest.Orders, opt => opt.Ignore());
            CreateMap<Stock, StockDto>();

            CreateMap<Supplier, SupplierDto>()
                .ForMember(dest => dest.Stocks, opt => opt.MapFrom(src => src.Stocks));
            CreateMap<SupplierDto, Supplier>()
                .ForMember(dest => dest.Stocks, opt => opt.MapFrom(src => src.Stocks));
        }
    }
}
