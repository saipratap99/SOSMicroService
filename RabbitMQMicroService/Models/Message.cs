using System;
namespace RabbitMQMicroService.Models
{
	public class Message
	{
		public int SOSRequestId { get; set; }
        public int UserId { get; set; }
        public int PoliceId { get; set; }
        public string operation { get; set; } = null!;

    }
}

