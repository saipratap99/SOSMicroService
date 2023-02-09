using System;
namespace SOSReqQueueAPIService.Models
{
	public abstract class TimeStamps
	{
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}

