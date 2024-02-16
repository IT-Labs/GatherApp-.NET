namespace GatherApp.Services.Extensions
{
    public static class ParseEnumStringToInt
    {
        public static T ToEnum<T>(string value)
        {
            var enumType = typeof(T);

            if (!enumType.IsEnum)
            {
                throw new InvalidOperationException($"Type {typeof(T).Name} is not a  enum.");
            }

            value = string.Join("", value.Split(' '));

            if (string.IsNullOrEmpty(value))
            {
                return default;
            }

            return (T)Enum.Parse(enumType, value.Replace(".", ""), true);
        }
    }
}
