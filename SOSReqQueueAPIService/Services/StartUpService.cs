using System;

namespace SOSReqQueueAPIService.Services
{
	public class StartUpService: IHostedService
	{

        private readonly IConsumer _consumer;
		public StartUpService(IConsumer consumer)
		{
            this._consumer = consumer;
		}

        public async Task StartAsync(CancellationToken cancellationToken)
        {

            string queueName = "sqsqueue";
            await this._consumer.StartConsumer(queueName);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

