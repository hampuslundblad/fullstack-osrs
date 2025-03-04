using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetComp.Errors
{
    public class BossServiceError
    {
        public static BaseError ServiceError(string? message = null)
        {
            return BaseError.Failure("BossService.ServiceError", "Service error - " + message);
        }
    }
}
