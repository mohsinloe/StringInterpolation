Dictionary<string, string> values = new Dictionary<string, string>()
{
    { "name", "Jim" },
    { "age","20"}
};

var message = StringInterpolation.Interpolate.InterpolateString("Hello [name], your are [age] years old. [[Escape Sequence]]", values);
Console.WriteLine(message);