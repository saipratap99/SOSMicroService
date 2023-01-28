using System;
namespace UsersAPIService.Exceptions
{
    public class BaseException : Exception
    {
        public BaseException(string message) : base(message)
        {

        }
    }
}

