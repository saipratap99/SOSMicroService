using System;
namespace SOSRequestsAPIService.Exceptions
{
    public class BaseException : Exception
    {
        public BaseException(string message) : base(message)
        {

        }
    }
}

