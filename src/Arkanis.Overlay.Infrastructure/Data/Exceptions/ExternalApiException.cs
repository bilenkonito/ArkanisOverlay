namespace Arkanis.Overlay.Infrastructure.Data.Exceptions;

public class ExternalApiException(string message, Exception? innerException) : Exception(message, innerException);
