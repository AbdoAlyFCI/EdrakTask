using AutoMapper;
using EdrakTask.Core.Domain;
using EdrakTask.Core.Dtos;

namespace EdrakTask.Core.Profiles
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Order, OrderDto>();
            CreateMap<OrderLine, OrderLineDto>();
        }
    }
}
