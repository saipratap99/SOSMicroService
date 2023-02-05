using System;
using FIRAPIService.Models;

namespace FIRAPIService.Services
{
	public interface IFIRService
	{
        public Task<string> Create(FIR fir);
        public Task<List<FIR>> Get();
        public Task<FIR> Get(int id);
        public Task<string> Delete(int id);
        public Task<string> Update(int id, FIR fir);
    }
}

