using System.Text;

namespace StringInterpolation
{
    public class Interpolate
    {
        const char openingBracket = '[';
        const char closingBracket = ']';
        public static string InterpolateString(string input, Dictionary<string, string> values)
        {
            StringBuilder sb = new StringBuilder();
            int currentIndex = 0;

            while (currentIndex < input.Length)
            {
                if (currentIndex == 0 && input.Length >= 2 && input[currentIndex] == '[' && input[currentIndex + 1] == '[')
                {
                    sb.Append(input.Substring(currentIndex));
                    break;
                }

                if (input[currentIndex] == openingBracket)
                {
                    int closingBracketIndex = IndexOfClosingBracket(input, currentIndex);

                    if (closingBracketIndex == -1)
                    {
                        throw new FormatException("Unclosed substitution token in string: " + input);
                    }

                    string token = GetToken(input, currentIndex, closingBracketIndex);
                    string replacement = GetReplacement(values, token);

                    sb.Append(replacement);

                    currentIndex = closingBracketIndex + 1;
                }
                else
                {
                    sb.Append(input[currentIndex]);
                    currentIndex++;
                }
            }

            return sb.ToString();
        }

        static int IndexOfClosingBracket(string input, int currentIndex)
        {
            int length = input.Length;
            for (int i = currentIndex + 1; i < length; i++)
            {
                if (input[i] == closingBracket)
                {
                    return i;
                }
            }
            return -1;
        }

        static string GetToken(string input, int startIndex, int endIndex)
        {
            return input.Substring(startIndex + 1, endIndex - startIndex - 1);
        }

        static string GetReplacement(Dictionary<string, string> values, string token)
        {
            if (values.TryGetValue(token, out string? value))
            {
                return value ?? throw new InvalidOperationException("Value retrieved from dictionary is null.");
            }
            return token;
        }
    }
}
