namespace UniversityManagementSystem.Application.Common;

public class Result
{
    public bool Succeeded { get; }
    public string[] Errors { get; }
    public string Message { get; }

    protected Result(bool succeeded, IEnumerable<string> errors, string message)
    {
        Succeeded = succeeded;
        Errors = errors.ToArray();
        Message = message;
    }

    public static Result Success(string message = "") => new(true, Array.Empty<string>(), message);
    public static Result Failure(IEnumerable<string> errors) => new(false, errors, "");
    public static Result Failure(string error) => new(false, new[] { error }, "");
}

public class Result<T> : Result
{
    public T? Data { get; }

    protected Result(bool succeeded, T? data, IEnumerable<string> errors, string message) 
        : base(succeeded, errors, message)
    {
        Data = data;
    }

    public static Result<T> Success(T data, string message = "") => new(true, data, Array.Empty<string>(), message);
    public static new Result<T> Failure(IEnumerable<string> errors) => new(false, default, errors, "");
    public static new Result<T> Failure(string error) => new(false, default, new[] { error }, "");
}
