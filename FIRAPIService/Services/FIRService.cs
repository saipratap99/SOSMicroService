using System;
using System.ComponentModel.DataAnnotations;
using FIRAPIService.Constants;
using FIRAPIService.Exceptions;
using FIRAPIService.Models;
using FIRAPIService.Repositories;
using FIRAPIService.Utils;
using RabbitMQMicroService.Constants;
using RabbitMQMicroService.Models;
using RabbitMQMicroService.Services;

namespace FIRAPIService.Services
{
	public class FIRService: IFIRService
	{
        private readonly IFIRRepository _fIRRepository;
        private readonly ILogger<FIRService> _logger;
        private IConfiguration _config;

        public FIRService(IFIRRepository fIRRepository, ILogger<FIRService> logger, IConfiguration config)
        {
            this._logger = logger;
            this._fIRRepository = fIRRepository;
            this._config = config;
        }

        public async Task<string> Create(FIR fIR)
        {
            try
            {
                this._logger.LogInformation($"Enter Services.FIRService.Create, FIR: {fIR}");
                ICollection<ValidationResult> results;
                if (!Helper.Validate<FIR>(fIR, out results))
                {
                    string errorMessages = String.Join("\n", results.Select(o => o.ErrorMessage));
                    throw new BusinessException(errorMessages);
                }

                fIR.StatusId = 4;
                this._logger.LogInformation($"Exit Services.FIRService.Create");
                fIR.Id = 0;

                string response = await this._fIRRepository.Create(fIR);
                SOSRequest sOSRequest = await this._fIRRepository.GetSOSRequestDetailsOfFir(fIR.SOSRequestId);

                // Publish to QUEUE
                string exchangeName = this._config.GetValue<string>("RabbitMQConstants:exchangeName");
                string routingKey = this._config.GetValue<string>("RabbitMQConstants:routingKey");
                string queueName = this._config.GetValue<string>("RabbitMQConstants:queueName");
                Console.WriteLine($"${exchangeName} - ${routingKey}");
                RabbitMQService rabbitMQService = new RabbitMQService();
                Message msg = new Message()
                {
                    operation = Operations.reqClosed,
                    UserId = sOSRequest.UserId,
                    SOSRequestId = (int)sOSRequest.Id,
                    PoliceId = (int)sOSRequest.PoliceId,
                };

                rabbitMQService.PublishMessage(msg, exchangeName, routingKey, queueName);
                return response;
            }
            catch (Exception e)
            {
                this._logger.LogError($"Error: Services.FIRService.Create, Error: {e.Message}");
                throw;
            }
        }

        public Task<string> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<FIR>> Get()
        {
            try
            {
                this._logger.LogInformation($"Enter Services.FIRService.Get.");
                List<FIR> fIRs = await this._fIRRepository.Get();
                this._logger.LogInformation($"Exit Services.FIRService.Get, FIR {fIRs}");
                return fIRs;
            }
            catch (Exception e)
            {
                this._logger.LogError($"Error: Services.FIRService.Get, Error: {e.Message}");
                throw;
            }
        }

        public async Task<FIR> Get(int id)
        {
            try
            {
                this._logger.LogInformation($"Enter Services.FIRService.Get, Id: {id}");
                FIR fir = await this._fIRRepository.Get(id);
                this._logger.LogInformation($"Exit Services.FIRService.Get, FIR {fir}");
                return fir;
            }
            catch (Exception e)
            {
                this._logger.LogError($"Error: Services.FIRService.Get, Error: {e.Message}");
                throw;
            }
        }

        public Task<string> Update(int id, FIR fIR)
        {
            throw new NotImplementedException();
        }
    }
}

