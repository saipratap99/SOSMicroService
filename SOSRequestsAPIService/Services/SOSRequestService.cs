using System;
using System.ComponentModel.DataAnnotations;
using SOSRequestsAPIService.Models;
using SOSRequestsAPIService.Repositories;
using SOSRequestsAPIService.Exceptions;
using SOSRequestsAPIService.Services;
using SOSRequestsAPIService.Utils;

namespace SOSRequestsAPIService.Services
{
	public class SOSRequestService: ISOSRequestService
	{

        private readonly ISOSRequestRepository _sOSRequestRepository;
        private readonly ILogger<SOSRequestService> _logger;

        public SOSRequestService(ISOSRequestRepository sOSRequestRepository, ILogger<SOSRequestService> logger)
        {
            this._logger = logger;
            this._sOSRequestRepository = sOSRequestRepository;
        }

        public async Task<string> Create(SOSRequest sOSRequest)
        {
            try
            {
                this._logger.LogInformation($"Enter Services.SOSRequestsAPIService.Create, SOS Request: {sOSRequest}");
                ICollection<ValidationResult> results;
                if (!Helper.Validate<SOSRequest>(sOSRequest, out results))
                {
                    string errorMessages = String.Join("\n", results.Select(o => o.ErrorMessage));
                    throw new BusinessException(errorMessages);
                }
                this._logger.LogInformation($"Exit Services.SOSRequestsAPIService.Create");
                sOSRequest.Id = 0;
                
                string response = await this._sOSRequestRepository.Create(sOSRequest);
                return response;
            }
            catch (Exception e)
            {
                this._logger.LogError($"Error: Services.SOSRequestsAPIService.Create, Error: {e.Message}");
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
                this._logger.LogInformation($"Enter Services.SOSRequestsAPIService.Get.");
                List<SOSRequest> sOSRequests = await this._sOSRequestRepository.Get();
                this._logger.LogInformation($"Exit Services.SOSRequestsAPIService.Get, SOS Requests {sOSRequests}");
                return sOSRequests;
            }
            catch (Exception e)
            {
                this._logger.LogError($"Error: Services.SOSRequestsAPIService.Get, Error: {e.Message}");
                throw;
            }
        }

        public async Task<SOSRequest> Get(int id)
        {
            try
            {
                this._logger.LogInformation($"Enter Services.SOSRequestsAPIService.Get, Id: {id}");
                SOSRequest sOSRequest = await this._sOSRequestRepository.Get(id);
                this._logger.LogInformation($"Exit Services.SOSRequestsAPIService.Get, SOS Request {sOSRequest}");
                return sOSRequest;
            }
            catch (Exception e)
            {
                this._logger.LogError($"Error: Services.SOSRequestsAPIService.Get, Error: {e.Message}");
                throw;
            }
        }

        public Task<string> Update(int id, SOSRequest sOSRequest)
        {
            throw new NotImplementedException();
        }
    }
}

