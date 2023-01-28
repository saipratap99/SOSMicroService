using System;
using UsersAPIService.Models;

namespace UsersAPIService.Repositories
{
    public interface IUserRepository
    {
        public Task<string> CreateUser(User user);
        public Task<List<User>> GetUsers();
        public Task<User> GetUser(int id);
        public Task<User> GetUser(string email);
    }
}


