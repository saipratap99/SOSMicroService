using System;
using UsersAPIService.Constants;

namespace UsersAPIService.Exceptions
{
    public class SystemException : BaseException
    {

        public int code { get; set; } = (int)StatusCodeEnums.SYSTEM_EXCEPTION;

        public SystemException(string message) : base(message)
        {

        }
    }
}

