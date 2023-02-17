namespace SampleBank.Domain.Exceptions.Base;

public abstract class BadRequestException : Exception
{
    /// <summary>
    /// Exception class for Bad request
    /// </summary>
    /// <param name="message"></param>
    protected BadRequestException(string message)
        : base(message)
    {
    }
}