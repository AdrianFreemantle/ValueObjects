using System;
using System.Linq;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace System
{
    internal static class StringExtensions
    {
        public static bool IsAllDigits(this string value)
        {
            return value.All(Char.IsDigit);
        }

        public static bool IsAllLetters(this string value)
        {
            return value.All(Char.IsLetter);
        }

        public static string GetAllDigits(this string value)
        {
            try
            {
                var result = value.Where(Char.IsDigit).ToArray();
                return new string(result);
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public static string SplitCamelCase(this string input)
        {
            return Regex.Replace(input, "([A-Z])", " $1", RegexOptions.Compiled).Trim();
        }

        public static string ToUriSafeString(this string value)
        {
            return Regex.Replace(value, "[^a-zA-Z0-9]", "-");
        }

        public static string ToAlphanumeric(this string value)
        {
            return Regex.Replace(value, "[^a-zA-Z0-9]", "");
        }

        public static string ReplaceFirstInstanceOf(this string value, char oldCharacter, char newCharacter)
        {
            var index = value.IndexOf(oldCharacter);

            if (index > 0)
            {
                var stringCharacters = value.ToCharArray();
                stringCharacters[index] = newCharacter;
                return new string(stringCharacters); 
            }

            return value;
        }

        public static string ReplaceLastInstanceOf(this string value, char oldCharacter, char newCharacter)
        {
            var index = value.LastIndexOf(oldCharacter);

            if (index > 0)
            {
                var stringCharacters = value.ToCharArray();
                stringCharacters[index] = newCharacter;
                return new string(stringCharacters);
            }

            return value;
        }
    }
}