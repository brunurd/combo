using System.Text;

namespace Combo.EnumFromJsonComboTask.Editor
{
    public static class StringExtensionMethods
    {
        public static string Capitalize(this string str)
        {
            var chars = str.ToCharArray();
            var capital = $"{str[0]}".ToUpper();
            chars[0] = capital[0];
            return new string(chars);
        }

        public static string ToPascalCase(this string str)
        {
            var words = str
                .Replace("-", " ")
                .Replace("_", " ")
                .Split(' ');

            var newStr = new StringBuilder();

            foreach (var word in words)
            {
                newStr.Append(word.Capitalize());
            }

            return newStr.ToString();
        }
    }
}