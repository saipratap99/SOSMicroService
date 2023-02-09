using System;
using System.ComponentModel.DataAnnotations;
using SOSReqQueueAPIService.Exceptions;
using SOSReqQueueAPIService.Models;
using SOSReqQueueAPIService.Repositories;
using SOSReqQueueAPIService.Utils;

namespace SOSReqQueueAPIService.Services
{
	public class SOSReqQueueService: ISOSReqQueueService
	{
        private readonly ISOSReqQueueRepository _sOSReqQueueRepository;
        private readonly ILogger<SOSReqQueueService> _logger;

        public SOSReqQueueService(ISOSReqQueueRepository sOSReqQueueRepository, ILogger<SOSReqQueueService> logger)
        {
            this._logger = logger;
            this._sOSReqQueueRepository = sOSReqQueueRepository;
        }

        public async Task<string> Create(SOSReqQueue sOSReqQueue)
        {
            try
            {
                this._logger.LogInformation($"Enter Services.SOSReqQueueService.Create, SOS Req Service: {sOSReqQueue}");
                ICollection<ValidationResult> results;
                if (!Helper.Validate<SOSReqQueue>(sOSReqQueue, out results))
                {
                    string errorMessages = String.Join("\n", results.Select(o => o.ErrorMessage));
                    throw new BusinessException(errorMessages);
                }
                this._logger.LogInformation($"Exit Services.SOSReqQueueService.Create");
                sOSReqQueue.Id = 0;

                string response = await this._sOSReqQueueRepository.Create(sOSReqQueue);
                return response;
            }
            catch (Exception e)
            {
                this._logger.LogError($"Error: Services.SOSReqQueueService.Create, Error: {e.Message}");
                throw;
            }
        }

        public async Task<string> Delete(int id)
        {
            try
            {
                this._logger.LogInformation($"Enter Services.SOSReqQueueService.Delete, Id: {id}");
                var response = await this._sOSReqQueueRepository.Delete(id);
                this._logger.LogInformation($"Exit Services.SOSReqQueueService.Delete, Response {response}");
                return response;
            }
            catch (Exception e)
            {
                this._logger.LogError($"Error: Services.SOSReqQueueService.Delete, Error: {e.Message}");
                throw;
            }
        }

        public async Task<List<SOSReqQueue>> Get()
        {
            try
            {
                this._logger.LogInformation($"Enter Services.SOSReqQueueService.Get.");
                List<SOSReqQueue> sOSReqQueue = await this._sOSReqQueueRepository.Get();
                this._logger.LogInformation($"Exit Services.SOSReqQueueService.Get, SOS Req Queue {sOSReqQueue}");
                return sOSReqQueue;
            }
            catch (Exception e)
            {
                this._logger.LogError($"Error: Services.SOSReqQueueService.Get, Error: {e.Message}");
                throw;
            }
        }

        public async Task<SOSReqQueue> Get(int id)
        {
            try
            {
                this._logger.LogInformation($"Enter Services.SOSReqQueueService.Get, Id: {id}");
                SOSReqQueue sOSReqQueue = await this._sOSReqQueueRepository.Get(id);
                this._logger.LogInformation($"Exit Services.SOSReqQueueService.Get, SOS Req QUeue {sOSReqQueue}");
                return sOSReqQueue;
            }
            catch (Exception e)
            {
                this._logger.LogError($"Error: Services.SOSReqQueueService.Get, Error: {e.Message}");
                throw;
            }
        }

        public Task<string> Update(int id, SOSReqQueue sOSReqQueue)
        {
            throw new NotImplementedException();
        }
    }
}

