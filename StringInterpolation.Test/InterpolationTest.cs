
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.Interfaces;

namespace StringInterpolation.Test
{
    public class InterpolationTest
    {
        [Fact]
        public void Interpolate_NoSubstitution_ReturnsInputString()
        {
            string input = "Hello Jim!";
            Dictionary<string, string> values = new Dictionary<string, string>();

            string result = Interpolate.InterpolateString(input, values);

            Assert.Equal(input, result);
        }

        [Fact]
        public void InterpolateS_WithSubstitution_ReturnsInterpolatedString()
        {
            string input = "Hello [name]!";
            Dictionary<string, string> values = new Dictionary<string, string>();
            values["name"] = "Jim";
            string expected = "Hello Jim!";

            string result = Interpolate.InterpolateString(input, values);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Interpolate_WithSubstitution_ReturnsEscapedInterpolatedString()
        {
            string input = "Hello [name] [[author]]!";
            Dictionary<string, string> values = new Dictionary<string, string>();
            values["name"] = "Jim";
            string expected = "Hello Jim [author]!";

            string result = Interpolate.InterpolateString(input, values);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Interpolate_UnclosedToken_ThrowsFormatException()
        {
            string input = "Hello [[name!";
            Dictionary<string, string> values = new Dictionary<string, string>();

            Assert.Throws<FormatException>(() => Interpolate.InterpolateString(input, values));
        }
    }
}