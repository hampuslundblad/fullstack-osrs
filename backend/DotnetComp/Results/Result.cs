using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotnetComp.Errors;

namespace DotnetComp.Results
{
    public sealed class Result<TValue> : BaseResult
    {
        public TValue Value =>
            IsSuccess
                ? _value
                : throw new InvalidOperationException(
                    "Value cannot be accessed on a failed result"
                );

        public static implicit operator Result<TValue>(BaseError error) => new(error);

        public static implicit operator Result<TValue>(TValue value) => new(value);

        public static Result<TValue> Success(TValue value) => new(value);

        public static new Result<TValue> Failure(BaseError error) => new(error);

        private readonly TValue _value;

        private Result(TValue value)
            : base()
        {
            _value = value;
        }

        private Result(BaseError error)
            : base(error)
        {
            _value = default!;
        }
    }
}
