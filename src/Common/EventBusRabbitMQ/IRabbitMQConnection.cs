using System;
using RabbitMQ.Client;

namespace EventBusRabbitMQ
{
    public interface IRabbitMQConnection : IDisposable
    {
        bool isConnected { get; }
        bool TryConnect();
        IModel CreateModel();


    }
}