using System;
using System.ComponentModel.DataAnnotations;
using FIRAPIService.Exceptions;
using FIRAPIService.Models;
using FIRAPIService.Repositories;
using FIRAPIService.Utils;

namespace FIRAPIService.Services
{
	public class FIRService: IFIRService
	{
        private readonly IFIRRepository _fIRRepository;
        private readonly ILogger<FIRService> _logger;

        public FIRService(IFIRRepository fIRRepository, ILogger<FIRService> logger)
        {
            this._logger = logger;
            this._fIRRepository = fIRRepository;
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
                this._logger.LogInformation($"Exit Services.FIRService.Create");
                fIR.Id = 0;

                string response = await this._fIRRepository.Create(fIR);
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

