using GO.HostBuilder.Enums;
using GO.HostBuilder.Resources;
using Microsoft.AspNetCore.Http;

namespace GO.HostBuilder.Exceptions
{
    public sealed class GoForbiddenException
        : GoException
    {
        public GoForbiddenException()
            : base(
                StatusCodes.Status403Forbidden,
                ExceptionType.Forbidden,
                ValidationResource.Forbidden)
        {
        }
    }
}
