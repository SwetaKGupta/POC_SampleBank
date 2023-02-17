namespace SampleBank.Domain.Exceptions.Base;

/// <summary>
/// Base class for NotFound Exception
/// </summary>
public abstract class NotFoundException : Exception
{
    protected NotFoundException(string message)
        : base(message)
    {
    }
}