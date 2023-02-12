using RabbitMQ.Client;
using RabbitMQMicroService.Services;
using RabbitMQMicroService.Models;
using RabbitMQMicroService.Constants;
// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");


//RabbitMQService rabbitMQService = new RabbitMQService();

// Create SOS Exchange
string exchangeName = "SOSExchange";
//rabbitMQService.createDirectExchange(exchangeName);

// Creating SOS Queue
string queueName = "sqsqueue";
//rabbitMQService.QueueDeclare(queueName);

// Create binding for exhange and queue
string routingKey = "directexchange_key";
//rabbitMQService.QueueBind(queueName, exchangeName, routingKey);

Message msg = new Message()
{
    SOSRequestId = 987,
    UserId = 1,
    PoliceId = 2,
    operation = Operations.assignPolice
};

//rabbitMQService.StartConsumer(queueName);
//rabbitMQService.PublishMessage(msg, exchangeName, routingKey);

