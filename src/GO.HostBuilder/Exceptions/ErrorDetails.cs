using GO.HostBuilder.Enums;

namespace GO.HostBuilder.Exceptions
{
    public sealed record ErrorDetails
    {
        public int Code { get; init; }

        public string Message { get; init; }

        public string FieldName { get; init; }

        public ErrorDetails(ExceptionType code)
            : this(code, code.ToString())
        {
        }

        public ErrorDetails(ExceptionType code, string message)
            : this(code, message, string.Empty)
        {
        }

        public ErrorDetails(ExceptionType code, string message, string fieldName)
        {
            Code = (int)code;
            Message = message;
            FieldName = fieldName;
        }
    }
}
