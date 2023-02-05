using System;
using Microsoft.VisualBasic;
using SOSRequestsAPIService.Models;
using UsersAPIService.Models;
using UsersAPIService.Repositories;
using UsersAPIService.Constants;
using Microsoft.EntityFrameworkCore;
using UsersAPIService.Exceptions;

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
                List<SOSRequest> sOSRequests = await this._context.SOSRequests.Include(u => u.Priority).ToListAsync<SOSRequest>();
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
                var sOSRequest = await this._context.SOSRequests.FirstOrDefaultAsync(sos => sos.Id == id);
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
    }
}

