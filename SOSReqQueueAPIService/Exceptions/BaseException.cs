﻿using System;
namespace SOSReqQueueAPIService.Exceptions
{
    public class BaseException : Exception
    {
        public BaseException(string message) : base(message)
        {

        }
    }
}

