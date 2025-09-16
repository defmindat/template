using EatWise.Common.Domain;

namespace EatWise.Common.Application.Exceptions;

public sealed class EatWiseException(string requestName, Error? error = default, Exception? innerException = default)
    : Exception("Application exception", innerException)
{
    public string RequestName { get; } = requestName;
    public Error? Error { get; } = error;
}
