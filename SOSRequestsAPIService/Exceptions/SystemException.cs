using System;
using SOSRequestsAPIService.Constants;

namespace SOSRequestsAPIService.Exceptions
{
    public class SystemException : BaseException
    {

        public int code { get; set; } = (int)StatusCodeEnums.SYSTEM_EXCEPTION;

        public SystemException(string message) : base(message)
        {

        }
    }
}

