using System;
using System.Text;
using System.Threading.Channels;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQMicroService.Models;

namespace RabbitMQMicroService.Services
{
	public class RabbitMQService
	{
        string UserName;
        string Password;
        string HostName;

        public RabbitMQService()
		{
            UserName = "guest";
            Password = "guest";
            HostName = "localhost";
        }

        public void createDirectExchange(string exchangeName)
		{
            try
            {

                //Main entry point to the RabbitMQ .NET AMQP client
                var connectionFactory = new RabbitMQ.Client.ConnectionFactory()
                {
                    UserName = UserName,
                    Password = Password,
                    HostName = HostName
                };

                var connection = connectionFactory.CreateConnection();
                var model = connection.CreateModel();
                Console.WriteLine("Creating Exchange");
                // Create Exchange
                model.ExchangeDeclare(exchangeName, ExchangeType.Direct);
            }
            catch (Exception err)
            {
                Console.WriteLine(err);
                throw err;
            }
        }

        public void QueueDeclare(string queueName)
        {
            try
            {
                //Main entry point to the RabbitMQ .NET AMQP client
                var connectionFactory = new RabbitMQ.Client.ConnectionFactory()
                {
                    UserName = UserName,
                    Password = Password,
                    HostName = HostName
                };

                var connection = connectionFactory.CreateConnection();
                var model = connection.CreateModel();

                // Create Queue
                model.QueueDeclare(queueName, true, false, false, null);
                Console.WriteLine("Creating Queue");
            }
            catch(Exception err)
            {
                Console.WriteLine(err);
                throw;
            }
        }

        public void QueueBind(string queueName, string exchange, string routingKey)
        {
            try
            {
                //Main entry point to the RabbitMQ .NET AMQP client
                var connectionFactory = new RabbitMQ.Client.ConnectionFactory()
                {
                    UserName = UserName,
                    Password = Password,
                    HostName = HostName
                };

                var connection = connectionFactory.CreateConnection();
                var model = connection.CreateModel();

                // Bind Queue to Exchange
                model.QueueBind(queueName, exchange, routingKey);
                Console.WriteLine("Creating Binding");
                Console.ReadLine();
            }
            catch(Exception err)
            {
                Console.WriteLine(err);
                throw;
            }
        }

        public void PublishMessage(Message message, string exchangeName, string routingKey)
        {
            try
            {
                // Main entry point to the RabbitMQ.NET AMQP client
                var connectionFactory = new RabbitMQ.Client.ConnectionFactory()
                {
                    UserName = UserName,
                    Password = Password,
                    HostName = HostName
                };
                var connection = connectionFactory.CreateConnection();
                var model = connection.CreateModel();
                var properties = model.CreateBasicProperties();
                properties.Persistent = false;
                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
                Console.WriteLine("Body");
                model.BasicPublish(exchange: exchangeName, routingKey: routingKey, basicProperties: properties, body: body);
                Console.WriteLine("Message Sent");
                Console.ReadLine();
            }
            catch(Exception err)
            {
                Console.WriteLine(err);
                throw;
            }
        }

        public void StartConsumer(string queueName)
        {
            try
            {
                var connectionFactory = new RabbitMQ.Client.ConnectionFactory()
                {
                    UserName = UserName,
                    Password = Password,
                    HostName = HostName
                };
                var connection = connectionFactory.CreateConnection();
                var model = connection.CreateModel();
                var consumer = new EventingBasicConsumer(model);
                Console.WriteLine("Consumer");
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;
                    var messageJSON = Encoding.UTF8.GetString(body.ToArray());

                    var message = JsonConvert.DeserializeObject<Message>(messageJSON);
                    if (message != null)
                    {
                        //Me.Products.Add(product);
                        Console.WriteLine(" [x] Received {0}", message.SOSRequestId);
                    }

                };
                model.BasicConsume(queue: queueName,
                                     autoAck: true,
                                     consumer: consumer);

            }catch(Exception err)
            {
                Console.WriteLine(err);
                throw;
            }
        }
    
    }
}

