namespace Jira.Api.Infrastructure.Extensions
{
    public static class CommonExtensions
    {
        public static bool IsNullOrEmpty<T>(this T value)
        {
            if (typeof(T) == typeof(string)) return string.IsNullOrEmpty(value as string);

            return value == null || value.Equals(default(T));
        }
    }
}