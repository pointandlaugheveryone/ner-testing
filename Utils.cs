using DocumentFormat.OpenXml.Bibliography;
using System.Text.RegularExpressions;

namespace CVtesting;

public static class Utils {
    public static Dictionary<string,string> RegexContacts(string text) {
        Dictionary<string, string> results = [];

        string phonePattern = @"(?:420\s*)?(?<number>(?:\d\s*){9})";
        Regex phoneRegex = new(phonePattern);
        results["phone"] = phoneRegex.Match(text).Value;
        
        string emailPattern = @"[a-zA-Z0-9._%+\-]+@[a-zA-Z0-9.\-]+\.(?:cz|com)\b";
        Regex emailRegex = new(emailPattern, RegexOptions.IgnoreCase);
        results["email"] = emailRegex.Match(text).Value;

        string linkedinPattern = @"(http(s?)://)?(www\.)?linkedin\.([a-z])+/(in/)([A-Za-z0-9]+)+/?";
        Regex linkedinRegex = new(linkedinPattern, RegexOptions.IgnoreCase);
        results["linkedin"] = linkedinRegex.Match(text).Value;
        
        return results;
    }  // TODO: !if results[thing] == string.empty : display alert to user to add it
}
