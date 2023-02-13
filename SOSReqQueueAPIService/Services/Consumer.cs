using System;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQMicroService.Constants;
using RabbitMQMicroService.Models;
using SOSReqQueueAPIService.Constants;
using SOSReqQueueAPIService.Exceptions;
using SOSReqQueueAPIService.Models;

namespace SOSReqQueueAPIService.Services
{
    public static class Consumer
    {
       
        public async static Task<object> StartConsumer(string queueName, string connectionString)
        {
            try
            {
                var serverVersion = new MySqlServerVersion(new Version(8, 0, 28));
                var optionsBuilder = new DbContextOptionsBuilder<SOSDbContext>();
                optionsBuilder.UseMySql(connectionString, serverVersion, options => options.EnableRetryOnFailure());
                var _context = new SOSDbContext(optionsBuilder.Options);
                
                string UserName;
                string Password;
                string HostName;
                int Port;

                UserName = "RabbitMQPratap";
                Password = "RabbitMQPratap1012";
                Port = 5671;
                HostName = "b-ebbdd5f0-3ed6-4339-ba33-845043c615f7.mq.us-east-1.amazonaws.com";


                Console.WriteLine("Consumer Started");
                var connectionFactory = new ConnectionFactory()
                {
                    UserName = UserName,
                    Password = Password,
                    HostName = HostName,
                    Port = Port,
                    Ssl = {
                        Enabled = true,
                        ServerName = HostName
                    }

                };
                var connection = connectionFactory.CreateConnection();
                var model = connection.CreateModel();
                var consumer = new EventingBasicConsumer(model);
                
                consumer.Received += (async (model, ea) =>
                {
                    var body = ea.Body;
                    var messageJSON = Encoding.UTF8.GetString(body.ToArray());

                    var message = JsonConvert.DeserializeObject<Message>(messageJSON);
                    if (message != null)
                    {
                        if(message.operation == Operations.assignPolice)
                        {
                            Console.WriteLine("Assign Police");
                            _context.Add(new SOSReqQueue() { PoliceId = message.PoliceId, SOSRequestId = message.SOSRequestId, UserId = message.UserId });
                            await _context.SaveChangesAsync();
                            //await sOSReqQueueService.Create(new Models.SOSReqQueue() { PoliceId = message.PoliceId, SOSRequestId = message.SOSRequestId, UserId = message.UserId });
                            Console.WriteLine("Created Record");
                        }
                        if (message.operation == Operations.reqClosed)
                        {
                            Console.WriteLine("Request closed");
                            var sOSReqQueue = await _context.SOSReqQueue.FirstOrDefaultAsync(sos => sos.SOSRequestId == message.SOSRequestId && sos.PoliceId == message.PoliceId && sos.UserId == message.UserId);
                            if (sOSReqQueue == null)
                                throw new BusinessException($"{ResponseConstants.SOS_Request_Queue_NOT_FOUND} id: {message.SOSRequestId}");
                            _context.SOSReqQueue.Remove(sOSReqQueue);
                            await _context.SaveChangesAsync();

                            //await sOSReqQueueService.Delete(message.SOSRequestId);
                            Console.WriteLine("Deleted Record");
                        }
                        Console.WriteLine($" [x] Received {0} ${message.SOSRequestId} ${message.operation}");
                    }

                });
                model.BasicConsume(queue: queueName,
                                     autoAck: true,
                                     consumer: consumer);

            }
            catch (Exception err)
            {
                Console.WriteLine(err);
                throw;
            }

            return Task.CompletedTask;
        }
    }
}

