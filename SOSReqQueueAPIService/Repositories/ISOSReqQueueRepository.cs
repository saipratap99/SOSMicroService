using System;
using SOSReqQueueAPIService.Models;
namespace SOSReqQueueAPIService.Repositories
{
	public interface ISOSReqQueueRepository
	{
        public Task<string> Create(SOSReqQueue sOSReqQueue);
        public Task<List<SOSReqQueue>> Get();
        public Task<SOSReqQueue> Get(int id);
        public Task<string> Delete(int id);
        public Task<string> Update(int id, SOSReqQueue sOSReqQueue);
    }
}

