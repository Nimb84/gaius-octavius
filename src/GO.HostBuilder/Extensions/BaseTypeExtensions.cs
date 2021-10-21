namespace GO.HostBuilder.Extensions
{
    public static class BaseTypeExtensions
    {
        public static string ToAlphanumeric(this Guid value) =>
            value.ToString().Replace("-", string.Empty);
    }
}
