using System;
namespace SOSReqQueueAPIService.Services
{
	public interface IConsumer
	{
		public Task StartConsumer(string queue);

    }
}

