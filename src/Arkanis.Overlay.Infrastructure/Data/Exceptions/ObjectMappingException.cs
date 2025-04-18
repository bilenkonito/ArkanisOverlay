namespace Arkanis.Overlay.Infrastructure.Data.Exceptions;

public class ObjectMappingException(string message, Exception? exception) : Exception(message, exception);
