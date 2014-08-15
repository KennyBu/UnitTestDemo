using System;
using System.Globalization;

namespace LibraryToTest
{
    public static class StringExtensionMethods
    {
        public static string CapitalizeFirstLetter(this String input)
        {
            return string.IsNullOrEmpty(input) ? input : CultureInfo.CurrentCulture.TextInfo.ToTitleCase(input);
        }

        public static Guid ToGuid(this String input)
        {
            Guid guidValue;
            Guid.TryParse(input, out guidValue);
            return guidValue;
        }

        public static string Right(this string input, int numberCharacters)
        {
            return input.Substring(numberCharacters > input.Length ? 0 : input.Length - numberCharacters);
        }

        public static string ReplaceSafe(this string input, string oldCharacter, string newCharacter)
        {
            return string.IsNullOrWhiteSpace(input) ? null : input.Replace(oldCharacter, newCharacter);
        }
    }
}