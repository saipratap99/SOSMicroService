using System;
using SOSRequestsAPIService.Models;

namespace SOSRequestsAPIService.Services
{
	public interface ISOSRequestService
	{
        public Task<string> Create(SOSRequest sOSRequest);
        public Task<List<SOSRequest>> Get();
        public Task<SOSRequest> Get(int id);
        public Task<string> Delete(int id);
        public Task<string> Update(int id, SOSRequest sOSRequest);

    }
}

