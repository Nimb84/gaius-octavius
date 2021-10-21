using GO.HostBuilder.Enums;
using GO.HostBuilder.Resources;
using Microsoft.AspNetCore.Http;

namespace GO.HostBuilder.Exceptions
{
    public sealed class GoNotFoundException
        : GoException
    {
        public GoNotFoundException(string entityName)
            : base(
                StatusCodes.Status404NotFound,
                ExceptionType.NotFound,
                string.Format(ValidationResource.NotFound_Format, entityName))
        {
        }
    }
}
