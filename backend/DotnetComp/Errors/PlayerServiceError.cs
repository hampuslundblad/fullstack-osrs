using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetComp.Errors
{
    public static class PlayerServiceErrror
    {
        public static BaseError NotFound()
        {
            return BaseError.NotFound("PlayerServiceError.NotFound", "Player not found");
        }

        public static BaseError ServiceError()
        {
            return BaseError.Failure("PlayerServiceError.ServiceError", "Service error");
        }

        public static BaseError ConversionError()
        {
            return BaseError.Failure(
                "PlayerServiceError.ConversionError",
                "Error when calculating player hiscore"
            );
        }

        public static BaseError CreatePlayerError()
        {
            return BaseError.Failure(
                "PlayerServiceErrro.CreatePlayerError",
                "Unable to create player"
            );
        }
    }
}
