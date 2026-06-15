namespace Backend_Test.Common
{
    public enum ResultStatus
    {
        Success,
        NotFound,
        Invalid,
        Conflict
    }

    public class Result
    {
        public ResultStatus Status { get; protected set; }
        public string? Error { get; protected set; }
        public bool IsSuccess => Status == ResultStatus.Success;

        public static Result Success() => new() { Status = ResultStatus.Success };
        public static Result NotFound(string error) => new() { Status = ResultStatus.NotFound, Error = error };
        public static Result Invalid(string error) => new() { Status = ResultStatus.Invalid, Error = error };
        public static Result Conflict(string error) => new() { Status = ResultStatus.Conflict, Error = error };
    }

    public class Result<T> : Result
    {
        public T? Value { get; private set; }

        public static Result<T> Success(T value) => new() { Status = ResultStatus.Success, Value = value };
        public new static Result<T> NotFound(string error) => new() { Status = ResultStatus.NotFound, Error = error };
        public new static Result<T> Invalid(string error) => new() { Status = ResultStatus.Invalid, Error = error };
        public new static Result<T> Conflict(string error) => new() { Status = ResultStatus.Conflict, Error = error };
    }
}
