using System;
using SOSReqQueueAPIService.Constants;

namespace SOSReqQueueAPIService.Exceptions
{
    public class BusinessException : BaseException
    {
        public int code { get; set; } = (int)StatusCodeEnums.BUSINESS_EXCEPTION;
        public BusinessException(string message) : base(message)
        {
        }
    }
}

