using System;
namespace FIRAPIService.Exceptions
{
    public class BaseException : Exception
    {
        public BaseException(string message) : base(message)
        {

        }
    }
}

