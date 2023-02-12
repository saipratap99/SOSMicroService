using System;
using System.ComponentModel.DataAnnotations;
using SOSRequestsAPIService.Models;
using SOSRequestsAPIService.Repositories;
using SOSRequestsAPIService.Exceptions;
using SOSRequestsAPIService.Services;
using SOSRequestsAPIService.Utils;
using SOSRequestsAPIService.Constants;

using RabbitMQMicroService.Services;
using RabbitMQMicroService.Models;
using RabbitMQMicroService.Constants;

namespace SOSRequestsAPIService.Services
{
	public class SOSRequestService: ISOSRequestService
	{
        private readonly ISOSRequestRepository _sOSRequestRepository;
        private readonly ILogger<SOSRequestService> _logger;
        private IConfiguration _config;

        public SOSRequestService(ISOSRequestRepository sOSRequestRepository, ILogger<SOSRequestService> logger, IConfiguration config)
        {
            this._logger = logger;
            this._sOSRequestRepository = sOSRequestRepository;
            this._config = config;
        }

        public async Task<string> AssignPolice(int sOSRequestId, int policeId)
        {
            try
            {
                this._logger.LogInformation($"Enter Services.SOSRequestsAPIService.AssignPolice. SOS Request id ${sOSRequestId}");
                SOSRequest sOSRequest = await this._sOSRequestRepository.AssignPolice(sOSRequestId, policeId);
                this._logger.LogInformation($"Exit Services.SOSRequestsAPIService.AssignPolice, SOS Request {sOSRequest}");

                // Publish to QUEUE
                string exchangeName = this._config.GetValue<string>("RabbitMQConstants:exchangeName");
                string routingKey = this._config.GetValue<string>("RabbitMQConstants:routingKey");
                string queueName = this._config.GetValue<string>("RabbitMQConstants:queueName");
                Console.WriteLine($"${exchangeName} - ${routingKey}");
                RabbitMQService rabbitMQService = new RabbitMQService();
                Message message = new Message() { PoliceId = policeId, SOSRequestId = sOSRequestId, UserId = sOSRequest.UserId, operation = Operations.assignPolice };
                rabbitMQService.PublishMessage(message, exchangeName, routingKey, queueName);
                return ResponseConstants.POLICE_ASSIGNED;
            }
            catch (Exception e)
            {
                this._logger.LogError($"Error: Services.SOSRequestsAPIService.AssignPolice, Error: {e.Message}");
                throw;
            }
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

