using DocumentFormat.OpenXml.Bibliography;
using System.Text.RegularExpressions;

namespace CVtesting;

public class Utils {
    public string RegexContacts(string text) {
        var phonePattern = @"(?:420\s*)?(?<number>(?:\d\s*){9})";
        var phoneRegex = new Regex(phonePattern);
        
        var emailPattern = @"[a-zA-Z0-9._%+\-]+@[a-zA-Z0-9.\-]+\.(?:cz|com)\b";
        var emailRegex = new Regex(emailPattern, RegexOptions.IgnoreCase);

        var result = new List<string>();

        foreach (Match match in phoneRegex.Matches(text)) {
            var cleanPhone = Regex.Replace(match.Groups["number"].Value, @"\s+", "");
            result.Add($"Phone: {cleanPhone}");
        }

        foreach (Match match in emailRegex.Matches(text)) {
            result.Add($"Email: {match.Value}");
        }

        return string.Join("\n", result);
    }
}
