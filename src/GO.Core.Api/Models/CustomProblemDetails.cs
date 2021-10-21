using GO.HostBuilder.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace GO.Core.Api.Models
{
    public sealed class CustomProblemDetails
        : ProblemDetails
    {
        public List<ErrorDetails> Errors { get; set; } = new();
    }
}
