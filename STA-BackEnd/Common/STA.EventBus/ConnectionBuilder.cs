using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace STA.EventBus
{
    public class ConnectionBuilder
    {
        private readonly string hostAddress;

        public ConnectionBuilder(string hostAddress)
        {
            this.hostAddress = hostAddress;
        }

        public ConnectionFactory Get()
        {
            return new ConnectionFactory() { HostName = this.hostAddress };
        }

        public bool CreateQueues()
        {
            ConnectionFactory conFactory;
            IConnection? connection = null;
            IModel? channel = null;

            try
            {
                conFactory = this.Get();
                connection = conFactory.CreateConnection();
                channel = connection.CreateModel();

                channel.QueueDeclare(queue: MessageQueue.UserRegistration, durable: false, exclusive: false, autoDelete: false, arguments: null);
                channel.QueueDeclare(queue: MessageQueue.UserRegistrationResult, durable: false, exclusive: false, autoDelete: false, arguments: null);
                channel.QueueDeclare(queue: MessageQueue.UserValidation, durable: false, exclusive: false, autoDelete: false, arguments: null);
                channel.QueueDeclare(queue: MessageQueue.UserValidationResult, durable: false, exclusive: false, autoDelete: false, arguments: null);
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                if (channel != null && channel.IsOpen)
                    channel.Close();

                if (connection != null && connection.IsOpen)
                    connection.Close();
            }

            return true;
        }
    }
}
