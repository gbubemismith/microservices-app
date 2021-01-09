using System;
using System.Text;
using AutoMapper;
using EventBusRabbitMQ;
using EventBusRabbitMQ.Events;
using EventBusRabbitMQ.Helpers;
using MediatR;
using Newtonsoft.Json;
using Ordering.Application.Commands;
using Ordering.Core.Repositories;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Ordering.API.RabbitMQ
{
    public class EventBusRabbitMqConsumer
    {
        private readonly IRabbitMQConnection _connection;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepo;

        public EventBusRabbitMqConsumer(IRabbitMQConnection connection, IMediator mediator,
        IMapper mapper,
        IOrderRepository orderRepo)
        {
            _connection = connection;
            _mediator = mediator;
            _mapper = mapper;
            _orderRepo = orderRepo;
        }

        public void Consume()
        {

            var channel = _connection.CreateModel();
            channel.QueueDeclare(queue: EventBusConstants.BasketCheckoutQueue, durable: true, exclusive: false, autoDelete: false, arguments: null);

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += ReceivedEvent;

            channel.BasicConsume(queue: EventBusConstants.BasketCheckoutQueue, autoAck: true, consumer: consumer);


        }

        private async void ReceivedEvent(object sender, BasicDeliverEventArgs e)
        {
            if (e.RoutingKey == EventBusConstants.BasketCheckoutQueue)
            {
                var message = Encoding.UTF8.GetString(e.Body.Span);
                var basketCheckoutEvent = JsonConvert.DeserializeObject<BasketCheckoutEvent>(message);

                var command = _mapper.Map<CheckoutOrderCommand>(basketCheckoutEvent);
                var result = await _mediator.Send(command);

            }
        }

        public void Disconnect()
        {
            _connection.Dispose();
        }
    }
}