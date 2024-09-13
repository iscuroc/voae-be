using System.Text;
using System.Text.RegularExpressions;

namespace Application.Shared;

public static partial class Extensions
{
    public static List<string> MapStringToList(this string stringValue, string separator = "|")
    {
        return string.IsNullOrWhiteSpace(stringValue) ? [] : stringValue.Split(separator).ToList();
    }
    
    public static string MapListToString(this List<string> listValue, string separator = "|")
    {
        return listValue.Count <= 0 ? "" : string.Join(separator, listValue);
    }
    
    public static string Slugify(this string value, params string[] whitelistChars)
    {
        var normalizedText = value.ToLower().Normalize(NormalizationForm.FormD);
        
        var reg = OnlyCharactersRegex(whitelistChars);
        var onlyCharacters = reg.Replace(normalizedText, "");

        return onlyCharacters
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Aggregate(new StringBuilder(), (acc, word) => acc.Append(word).Append('-'))
            .ToString()
            .TrimEnd('-');
    }

    private static Regex OnlyCharactersRegex(IEnumerable<string> whitelistChars)
    {
        var whitelistPattern = string.Join("", whitelistChars.Select(Regex.Escape));
        return new Regex($"[^a-zA-Z0-9 {whitelistPattern}]");
    }
}