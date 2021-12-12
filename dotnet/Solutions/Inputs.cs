using System.Text.Json.Serialization;

namespace AdventOfCode.Solutions
{
    public class Inputs
    {
        [JsonPropertyName("actual_input")]
        public string ActualInput { get; set; }
        [JsonPropertyName("day")]
        public int Day { get; set; }
        [JsonPropertyName("first_test_result")]
        public int FirstTestResult { get; set; }
        [JsonPropertyName("second_test_result")]
        public int SecondTestResult { get; set; }
        [JsonPropertyName("test_input")]
        public string TestInput { get; set; }
    }
}