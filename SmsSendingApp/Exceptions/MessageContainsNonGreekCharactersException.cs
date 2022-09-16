namespace SmsSendingApp.Exceptions;

public class MessageContainsNonGreekCharactersException : Exception
{
    public override string Message { get; }

    public MessageContainsNonGreekCharactersException(string message)
    {
        Message = message;
    }
}