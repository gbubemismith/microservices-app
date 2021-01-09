using AutoMapper;
using EventBusRabbitMQ.Events;
using Ordering.Application.Commands;

namespace Ordering.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<BasketCheckoutEvent, CheckoutOrderCommand>().ReverseMap();
        }
    }
}