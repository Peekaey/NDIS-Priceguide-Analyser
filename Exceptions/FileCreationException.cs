namespace PricelistGenerator.Exceptions;

public class FileCreationException : Exception
{
    public FileCreationException()
    {
    }

    public FileCreationException(string message) : base(message)
    {
    }

    public FileCreationException(string message, Exception innerException) : base(message, innerException)
    {
    }
}