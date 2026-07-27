namespace Brokerage.Application.Exceptions;

public class ForbiddenException : Exception
{
    public ForbiddenException()
        : base("You do not have permission to access this resource.")
    {
    }
}
