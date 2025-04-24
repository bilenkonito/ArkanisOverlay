namespace Arkanis.Overlay.Infrastructure.Data.Exceptions;

public class ObjectMappingMissingLinkedRelatedObjectException(string message)
    : ObjectMappingException(message, null);
