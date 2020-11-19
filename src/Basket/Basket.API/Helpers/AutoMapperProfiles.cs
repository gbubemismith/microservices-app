using AutoMapper;
using Basket.API.Entities;
using EventBusRabbitMQ.Events;

namespace Basket.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<BasketCheckoutModel, BasketCheckoutEvent>().ReverseMap();
        }
    }
}