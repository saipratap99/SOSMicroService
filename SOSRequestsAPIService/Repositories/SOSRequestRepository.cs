using System;
using Microsoft.VisualBasic;
using SOSRequestsAPIService.Models;
using SOSRequestsAPIService.Repositories;
using SOSRequestsAPIService.Constants;
using Microsoft.EntityFrameworkCore;
using SOSRequestsAPIService.Exceptions;

namespace SOSRequestsAPIService.Repositories
{
	public class SOSRequestRepository: ISOSRequestRepository
	{

        private readonly Models.SOSDbContext _context;
        private readonly ILogger<SOSRequest> _logger;
        public SOSRequestRepository(Models.SOSDbContext context, ILogger<SOSRequest> logger)
		{
            this._context = context;
            this._logger = logger;
		}

        public async Task<SOSRequest> AssignPolice(int sOSRequestId, int policeId)
        {
            try
            {
                this._logger.LogInformation($"Enter: Repositories.SOSRequestRepository.AssignPolice, SOS Request Id: {sOSRequestId}");
                var sosRequest = await this.Get(sOSRequestId);
                sosRequest.PoliceId = policeId;
                //if (sosRequest.PoliceId != null)
                //{
                    this._context.Update(sosRequest);
                    await this._context.SaveChangesAsync();
                //}
                    

                this._logger.LogInformation($"Exit: Repositories.SOSRequestRepository.AssignPolice");
                return sosRequest;
            }
            catch (Exception e)
            {
                this._logger.LogError($"Error: Repositories.SOSRequestRepository.AssignPolice, Error: {e.Message}");
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<string> Create(SOSRequest sOSRequest)
        {
            try
            {
                this._logger.LogInformation($"Enter: Repositories.SOSRequestRepository.Create, SOS Request: {sOSRequest}");
                this._context.Add(sOSRequest);
                int result = await this._context.SaveChangesAsync();
                this._logger.LogInformation($"Exit: Repositories.SOSRequestRepository.Create");
                return ResponseConstants.CREATED_SUCCESSFULLY;
            }
            catch (Exception e)
            {
                this._logger.LogInformation($"Error: Repositories.SOSRequestRepository.Create, Error: {e.Message}");
                Console.WriteLine(e);
                throw;
            }
        }

        public Task<string> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<SOSRequest>> Get()
        {
            try
            {
                this._logger.LogInformation($"Enter: Repositories.SOSRequestRepository.Get.");
                List<SOSRequest> sOSRequests = await this._context.SOSRequests.Include(u => u.Priority).Include(u => u.Status).Include(u => u.User).Include(u => u.Police).ToListAsync<SOSRequest>();
                this._logger.LogInformation($"Exit: Repositories.SOSRequestRepository.Get, SOS Requests {sOSRequests}");
                return sOSRequests;
            }
            catch (Exception e)
            {
                this._logger.LogError($"Error: Repositories.SOSRequestRepository.Get, Error: {e.Message}");
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<SOSRequest> Get(int id)
        {
            try
            {
                this._logger.LogInformation($"Enter: Repositories.SOSRequestRepository.Get, Id: {id}");
                var sOSRequest = await this._context.SOSRequests.Include(u => u.Priority).Include(u => u.Status).Include(u => u.User).Include(u => u.Police).FirstOrDefaultAsync(sos => sos.Id == id);
                if (sOSRequest == null)
                    throw new BusinessException($"{ResponseConstants.SOSRequest_NOT_FOUND} id: {id}");
                this._logger.LogInformation($"Exit: Repositories.SOSRequestRepository.Get");
                return sOSRequest;
            }
            catch (Exception e)
            {
                this._logger.LogError($"Error: Repositories.SOSRequestRepository.Get, Error: {e.Message}");
                Console.WriteLine(e);
                throw;
            }
        }

        public Task<string> Update(int id, SOSRequest sOSRequest)
        {
            throw new NotImplementedException();
        }

        //private object getSOSRequestsIncludingForiegnKeyData()
        //{
        //    var obj = this._context.SOSRequests.Include(u => u.Priority).Include(u => u.Status).Include(u => u.User).Include(u => u.Police);
        //    return obj;
        //}
    }
}

