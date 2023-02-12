using System;
using SOSRequestsAPIService.Models;

namespace SOSRequestsAPIService.Repositories
{
	public interface ISOSRequestRepository
	{
        public Task<string> Create(SOSRequest sOSRequest);
        public Task<List<SOSRequest>> Get();
        public Task<SOSRequest> Get(int id);
        public Task<string> Delete(int id);
        public Task<string> Update(int id, SOSRequest sOSRequest);
        public Task<SOSRequest> AssignPolice(int sOSRequestId, int policeId);
    }
}

