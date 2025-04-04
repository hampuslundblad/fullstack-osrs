using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetComp.Errors
{
    public static class PlayerHiscoreError
    {
        public static BaseError NotFound()
        {
            return BaseError.NotFound("PlayerHiscoreError.NotFound", "Player not found");
        }

        public static BaseError ServiceError(string? message = null)
        {
            return BaseError.Failure(
                "PlayerHiscoreError.ServiceError",
                "Service error - " + message
            );
        }

        public static BaseError ConversionError()
        {
            return BaseError.Failure(
                "PlayerHiscoreError.ConversionError",
                "Error when calculating player hiscore"
            );
        }
    }
}
