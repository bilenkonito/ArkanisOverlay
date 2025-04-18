namespace Arkanis.Overlay.Infrastructure.Data.Exceptions;

public class ObjectMappingMissingDependentObjectException(string message)
    : ObjectMappingException(message, null);
