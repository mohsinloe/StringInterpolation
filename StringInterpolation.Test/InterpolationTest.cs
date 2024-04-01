
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.Interfaces;

namespace StringInterpolation.Test
{
    public class InterpolationTest
    {
        [Fact]
        public void Replaces_Single_Token()
        {
            string message = "Hello, [name]!";
            Dictionary<string, string> values = new Dictionary<string, string>() 
            { 
                { "name", "John" } 
            };

            string result = Interpolate.InterpolateString(message, values);

            Assert.Equal("Hello, John!", result);
        }

        [Fact]
        public void Replaces_Multiple_Tokens()
        {
            string message = "Hello, [name]! Today is [day], [date].";
            Dictionary<string, string> values = new Dictionary<string, string>()
            {
                { "name", "Jane" },
                { "day", "Monday" },
                { "date", "2024-04-01" }
            };

            string result = Interpolate.InterpolateString(message, values);

            Assert.Equal("Hello, Jane! Today is Monday, 2024-04-01.", result);
        }
        [Fact]
        public void Handles_Missing_Key()
        {
            string message = "Hello, [name]! Today is [unknown].";
            Dictionary<string, string> values = new Dictionary<string, string>() 
            { 
                { "name", "Alice" } 
            };

            string result = Interpolate.InterpolateString(message, values);

            Assert.Equal("Hello, Alice! Today is [unknown].", result);
        }
        [Fact]
        public void Handles_Empty_Value()
        {
            string message = "Hello, [name]! Today is [date].";
            Dictionary<string, string> values = new Dictionary<string, string>() 
            { 
                { "name", "Bob" }, 
                { "date", "" } 
            };

            string result = Interpolate.InterpolateString(message, values);

            Assert.Equal("Hello, Bob! Today is .", result);
        }
        [Fact]
        public void Handles_Escaped_Brackets()
        {
            string message = "This message contains [[brackets]].";
            Dictionary<string, string> values = new Dictionary<string, string>();

            string result = Interpolate.InterpolateString(message, values);

            Assert.Equal("This message contains [[brackets]].", result);
        }
        [Fact]
        public void Handles_Empty_String()
        {
            string message = "";
            Dictionary<string, string> values = new Dictionary<string, string>();

            string result = Interpolate.InterpolateString(message, values);

            Assert.Equal("", result);
        }
        [Fact]
        public void Handles_Case_Sensitivity()
        {
            string message = "Hello, [NAME]! Today is [date].";
            Dictionary<string, string> values = new Dictionary<string, string>() 
            { 
                { "name", "John" }, 
                { "date", "2024-04-01" } 
            };

            string result = Interpolate.InterpolateString(message, values);

            Assert.Equal("Hello, [NAME]! Today is 2024-04-01.", result); // "NAME" should not be replaced
        }
        [Fact]
        public void Handles_Multiple_Occurrences_Same_Token()
        {
            string message = "Greetings, [user]! Your ID is [id] and your username is also [id].";
            Dictionary<string, string> values = new Dictionary<string, string>() 
            { 
                { "user", "Alice" }, 
                { "id", "12345" } 
            };

            string result = Interpolate.InterpolateString(message, values);

            Assert.Equal("Greetings, Alice! Your ID is 12345 and your username is also 12345.", result); // Same tokens should be replaced
        }
        [Fact]
        public void Handles_Whitespace_In_Tokens()
        {
            string message = "Hello, [ name ]! Today is [ date  ].";
            Dictionary<string, string> values = new Dictionary<string, string>() 
            { 
                { "name", "Bob" }, 
                { "date", "2024-04-01" } 
            };

            string result = Interpolate.InterpolateString(message, values);

            Assert.Equal("Hello, Bob! Today is 2024-04-01.", result); // Whitespace should be ignored
        }
        [Fact]
        public void Handles_Empty_Dictionary()
        {
            string message = "Hello, [name]! Today is [date].";
            Dictionary<string, string> values = new Dictionary<string, string>();

            string result = Interpolate.InterpolateString(message, values);

            Assert.Equal("Hello, [name]! Today is [date].", result); // All tokens remain unchanged
        }
    }
}