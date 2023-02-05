using System;
using SOSRequestsAPIService.Constants;

namespace SOSRequestsAPIService.Exceptions
{
    public class BusinessException : BaseException
    {
        public int code { get; set; } = (int)StatusCodeEnums.BUSINESS_EXCEPTION;
        public BusinessException(string message) : base(message)
        {
        }
    }
}

