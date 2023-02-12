using System;
using FIRAPIService.Constants;
using FIRAPIService.Exceptions;
using FIRAPIService.Models;
using Microsoft.EntityFrameworkCore;

namespace FIRAPIService.Repositories
{
	public class FIRRepository: IFIRRepository 
	{
        private readonly Models.SOSDbContext _context;
        private readonly ILogger<FIRRepository> _logger;
        public FIRRepository(Models.SOSDbContext context, ILogger<FIRRepository> logger)
        {
            this._context = context;
            this._logger = logger;
        }

        public async Task<string> Create(FIR fir)
        {
            try
            {
                this._logger.LogInformation($"Enter: Repositories.FIRRepository.Create, FIR : {fir}");
                this._context.Add(fir);
                int result = await this._context.SaveChangesAsync();
                this._context.Entry<FIR>(fir).GetDatabaseValues();
                
                this._logger.LogInformation($"Exit: Repositories.FIRRepository.Create");
                return ResponseConstants.CREATED_SUCCESSFULLY;
            }
            catch (Exception e)
            {
                this._logger.LogInformation($"Error: Repositories.FIRRepository.Create, Error: {e.Message}");
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<SOSRequest> GetSOSRequestDetailsOfFir(int sosRequestId)
        {
            try
            {
                this._logger.LogInformation($"Enter: Repositories.FIRRepository.GetSOSRequestDetailsOfFir, FIR Id : {sosRequestId}");
                var fir = await this._context.FIRs.Include(u => u.SOSRequest).FirstOrDefaultAsync<FIR>(fir => fir.SOSRequestId == sosRequestId);
                if(fir == null)
                    throw new BusinessException($"{ResponseConstants.FIR_NOT_FOUND} id: {sosRequestId}");
                this._logger.LogInformation($"Exit: Repositories.FIRRepository.GetSOSRequestDetailsOfFir");
                return fir.SOSRequest;
            }
            catch (Exception e)
            {
                this._logger.LogInformation($"Error: Repositories.FIRRepository.GetSOSRequestDetailsOfFir, Error: {e.Message}");
                Console.WriteLine(e);
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
                this._logger.LogInformation($"Enter: Repositories.FIRRepository.Get.");
                List<FIR> firs = await this._context.FIRs.Include(u => u.Status)
                                                         .Include(u => u.SOSRequest)
                                                            .ThenInclude(u => u.User)
                                                         .Include(u => u.SOSRequest)
                                                            .ThenInclude(u => u.Police)
                                                         .Include(u => u.SOSRequest)
                                                            .ThenInclude(u => u.Status)
                                                         .Include(u => u.SOSRequest)
                                                            .ThenInclude(u => u.Priority)
                                                         .ToListAsync<FIR>();
                this._logger.LogInformation($"Exit: Repositories.FIRRepository.Get, FIR: {firs}");
                return firs;
            }
            catch (Exception e)
            {
                this._logger.LogError($"Error: Repositories.FIRRepository.Get, Error: {e.Message}");
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<FIR> Get(int id)
        {
            try
            {
                this._logger.LogInformation($"Enter: Repositories.FIRRepository.Get, Id: {id}");
                var fir = await this._context.FIRs.Include(u => u.Status)
                                                         .Include(u => u.SOSRequest)
                                                            .ThenInclude(u => u.User)
                                                         .Include(u => u.SOSRequest)
                                                            .ThenInclude(u => u.Police)
                                                         .Include(u => u.SOSRequest)
                                                            .ThenInclude(u => u.Status)
                                                         .Include(u => u.SOSRequest)
                                                            .ThenInclude(u => u.Priority)
                                                         .FirstOrDefaultAsync(sos => sos.Id == id);
                if (fir == null)
                    throw new BusinessException($"{ResponseConstants.FIR_NOT_FOUND} id: {id}");
                this._logger.LogInformation($"Exit: Repositories.FIRRepository.Get");
                return fir;
            }
            catch (Exception e)
            {
                this._logger.LogError($"Error: Repositories.FIRRepository.Get, Error: {e.Message}");
                Console.WriteLine(e);
                throw;
            }
        }

        public Task<string> Update(int id, FIR fir)
        {
            throw new NotImplementedException();
        }

    }
}

