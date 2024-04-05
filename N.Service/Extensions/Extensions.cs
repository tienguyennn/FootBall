namespace N.Extensions
{
    public static class Extensions
    {
        public static bool HasValue(this string? value)
            => !string.IsNullOrEmpty(value);

        public static string? Replace(this string? input, string? oldValue, string? newValue)
            => input?.Replace(oldValue ?? string.Empty, newValue ?? string.Empty);
    }
}
