using System.Text;
using System.Text.RegularExpressions;

namespace StringInterpolation
{
    public class Interpolate
    {
        public static string InterpolateString(string input, Dictionary<string, string> values)
        {
            // Regex pattern for finding tokens within square brackets
            const string pattern = @"\[([^\]]*)\]";

            return Regex.Replace(input, pattern, match =>
            {
                // Extract the key from the match (preserve case)
                string key = match.Groups[1].Value.Trim();

                // Check if key exists in the dictionary (case-sensitive)
                if (values.ContainsKey(key))
                {
                    // Replace token with value
                    return values[key];
                }
                else
                {
                    // Key not found, handle as literal
                    return match.Value;
                }
            });
        }
    }
}
