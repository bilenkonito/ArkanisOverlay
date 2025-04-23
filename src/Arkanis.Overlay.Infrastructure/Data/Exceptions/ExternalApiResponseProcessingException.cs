namespace Arkanis.Overlay.Infrastructure.Data.Exceptions;

public class ExternalApiResponseProcessingException(string message, Exception? innerException = null)
    : ExternalApiException(message, innerException);
