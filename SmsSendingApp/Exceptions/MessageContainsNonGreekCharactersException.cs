namespace SmsSendingApp.Exceptions;

public class MessageContainsNonGreekCharactersException : Exception
{
    public string Message { get; }

    public MessageContainsNonGreekCharactersException(string message)
    {
        Message = message;
    }
}