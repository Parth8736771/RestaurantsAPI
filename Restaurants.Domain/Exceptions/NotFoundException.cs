namespace Restaurants.Domain.Exceptions
{
    public class NotFoundException(string resourcesType, string resourceIdentifier) : Exception($"{resourcesType} with id: {resourceIdentifier} doesn't exist")
    {
    }
}
