using System;
using UsersAPIService.Constants;

namespace UsersAPIService.Exceptions
{
    public class BusinessException : BaseException
    {
        public int code { get; set; } = (int)StatusCodeEnums.BUSINESS_EXCEPTION;
        public BusinessException(string message) : base(message)
        {
        }
    }
}

