using System;
using Microsoft.EntityFrameworkCore;
using SOSReqQueueAPIService.Constants;
using SOSReqQueueAPIService.Exceptions;
using SOSReqQueueAPIService.Models;
namespace SOSReqQueueAPIService.Repositories
{
    public class SOSReqQueueRepository : ISOSReqQueueRepository
    {
        private readonly Models.SOSDbContext _context;
        private readonly ILogger<SOSReqQueueRepository> _logger;
        public SOSReqQueueRepository(Models.SOSDbContext context, ILogger<SOSReqQueueRepository> logger)
        {
            this._context = context;
            this._logger = logger;
        }

        public async Task<string> Create(SOSReqQueue sOSReqQueue)
        {
            try
            {
                this._logger.LogInformation($"Enter: Repositories.SOSReqQueueRepository.Create, SOS Request Queue : {sOSReqQueue}");
                this._context.Add(sOSReqQueue);
                int result = await this._context.SaveChangesAsync();
                this._logger.LogInformation($"Exit: Repositories.SOSReqQueueRepository.Create");
                return ResponseConstants.CREATED_SUCCESSFULLY;
            }
            catch (Exception e)
            {
                this._logger.LogInformation($"Error: Repositories.SOSReqQueueRepository.Create, Error: {e.Message}");
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<string> Delete(int id)
        {
            try
            {
                this._logger.LogInformation($"Enter: Repositories.SOSReqQueueRepository.Delete. id ${id}");
                var sOSReqQueue = await this._context.SOSReqQueue.Include(u => u.Status)
                                                        .Include(u => u.User)
                                                        .Include(u => u.Police)
                                                        .Include(u => u.SOSRequest)
                                                           .ThenInclude(u => u.Priority)
                                                        .FirstOrDefaultAsync(sos => sos.Id == id);
                if (sOSReqQueue == null)
                    throw new BusinessException($"{ResponseConstants.SOS_Request_Queue_NOT_FOUND} id: {id}");
                this._context.SOSReqQueue.Remove(sOSReqQueue);
                await this._context.SaveChangesAsync();
                this._logger.LogInformation($"Exit: Repositories.SOSReqQueueRepository.Delete");
                return ResponseConstants.DELETED_SUCCESSFULLY;

            }
            catch (Exception e)
            {
                this._logger.LogError($"Error: Repositories.SOSReqQueueRepository.Get, Error: {e.Message}");
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<List<SOSReqQueue>> Get()
        {
            try
            {
                this._logger.LogInformation($"Enter: Repositories.SOSReqQueueRepository.Get.");
                List<SOSReqQueue> sOSReqQueue = await this._context.SOSReqQueue
                                                                            .Include(u => u.User)
                                                                            .Include(u => u.Police)
                                                                            .Include(u => u.SOSRequest)
                                                                                .ThenInclude(u => u.Priority)
                                                                            .ToListAsync<SOSReqQueue>();
                this._logger.LogInformation($"Exit: Repositories.SOSReqQueueRepository.Get, SOS Request Queue: {sOSReqQueue}");
                return sOSReqQueue;
                
            }
            catch (Exception e)
            {
                this._logger.LogError($"Error: Repositories.SOSReqQueueRepository.Get, Error: {e.Message}");
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<SOSReqQueue> Get(int id)
        {
            try
            {
                this._logger.LogInformation($"Enter: Repositories.SOSReqQueueRepository.Get, Id: {id}");
                var sOSReqQueue = await this._context.SOSReqQueue.Include(u => u.Status)
                                                         .Include(u => u.User)
                                                         .Include(u => u.Police)
                                                         .Include(u => u.SOSRequest)
                                                            .ThenInclude(u => u.Priority)
                                                         .FirstOrDefaultAsync(sos => sos.Id == id);
                if (sOSReqQueue == null)
                    throw new BusinessException($"{ResponseConstants.SOS_Request_Queue_NOT_FOUND} id: {id}");
                this._logger.LogInformation($"Exit: Repositories.SOSReqQueueRepository.Get");
                return sOSReqQueue;
            }
            catch (Exception e)
            {
                this._logger.LogError($"Error: Repositories.SOSReqQueueRepository.Get, Error: {e.Message}");
                Console.WriteLine(e);
                throw;
            }
        }

        public Task<string> Update(int id, SOSReqQueue sOSReqQueue)
        {
            throw new NotImplementedException();
        }

    }
}