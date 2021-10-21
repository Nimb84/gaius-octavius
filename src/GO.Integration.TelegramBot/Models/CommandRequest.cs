using System.Text.RegularExpressions;
using GO.HostBuilder.Enums;
using GO.HostBuilder.Exceptions;
using GO.HostBuilder.Extensions;
using GO.Integration.TelegramBot.Behaviors.Management.Enums;
using Microsoft.AspNetCore.Http;

namespace GO.Integration.TelegramBot.Models
{
    internal sealed record CommandRequest
    {
        private const string ParsePattern = @"^\/(\w+|\?)\s*(\w*)\s*(.*?)$";

        public CommandType Type { get; }

        public string Action { get; }

        public List<string> Arguments { get; } = new();

        public CommandRequest(string message)
        {
            var matches = Regex.Matches(message, ParsePattern);

            if (!matches.Any())
                throw new GoException(
                    StatusCodes.Status400BadRequest,
                    ExceptionType.Cast);

            Type = EnumExtensions.Parse<CommandType>(matches[0].Groups[1].Value.Trim().ToLower());
            Action = matches[0].Groups[2].Value.Trim().ToLower();

            var argsString = matches[0].Groups[3].Value.Trim().ToLower();
            if (!string.IsNullOrWhiteSpace(argsString))
            {
                Arguments = argsString.Split()
                    .Where(arg => !string.IsNullOrWhiteSpace(arg))
                    .ToList();
            }
        }
    }
}
