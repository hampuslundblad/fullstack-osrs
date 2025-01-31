using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetComp.Errors
{
    public class BaseError
    {
        public string Code { get; }
        public string Description { get; }

        public ErrorType ErrorType { get; }

        public static BaseError Failure(string code, string description) =>
            new(code, description, ErrorType.Failure);

        public static BaseError NotFound(string code, string description) =>
            new(code, description, ErrorType.NotFound);

        public static BaseError Validation(string code, string description) =>
            new(code, description, ErrorType.Validation);

        public static BaseError Conflict(string code, string description) =>
            new(code, description, ErrorType.Conflict);

        private BaseError(string code, string description, ErrorType errorType)
        {
            Code = code;
            Description = description;
            ErrorType = errorType;
        }
    }
}
