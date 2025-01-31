using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetComp.Errors
{
    public class GroupServiceError
    {
        public static BaseError ErrorWhenCreatingGroup(string GroupName)
        {
            return BaseError.Failure(
                "GroupServiceError.Failure",
                $"There was an error when creating the group {GroupName}"
            );
        }
    }
}
