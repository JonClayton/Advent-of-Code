using System.Text.Json.Serialization;

namespace AdventOfCode2021.Classes
{
    public class Inputs
    {
        [JsonPropertyName("actual_input")]
        public string ActualInput { get; set; }
        [JsonPropertyName("day")]
        public int Day { get; set; }
        [JsonPropertyName("first_test_result")]
        public long FirstTestResult { get; set; }
        [JsonPropertyName("second_test_result")]
        public long SecondTestResult { get; set; }
        [JsonPropertyName("test_input")]
        public string TestInput { get; set; }
    }
}