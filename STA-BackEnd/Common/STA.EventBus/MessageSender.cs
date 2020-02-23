using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace STA.EventBus
{
    public class MessageSender
    {
        private ConnectionBuilder builder;

        public MessageSender(ConnectionBuilder builder)
        {
            this.builder = builder;
        }

        public void Send<T>(T message, string queueName)
        {
            ConnectionFactory conFactory;
            IConnection? connection = null;
            IModel? channel = null;

            try
            {
                conFactory = builder.Get();
                connection = conFactory.CreateConnection();
                channel = connection.CreateModel();

                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

                channel.BasicPublish(exchange: "",
                                     routingKey: queueName,
                                     basicProperties: null,
                                     body: body);
            }
            catch (Exception ex)
            {
                return;
            }
            finally
            {
                if (channel != null && channel.IsOpen)
                    channel.Close();

                if (connection != null && connection.IsOpen)
                    connection.Close();
            }
        }
    }
}
