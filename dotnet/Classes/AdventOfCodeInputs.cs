using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AdventOfCode.Classes;

public class AdventOfCodeInputs
{
    [JsonPropertyName("actual_input")]
    public string ActualInput { get; set; }

    [JsonPropertyName("second_actual_input")]
    public string SecondActualInput { get; set; }
    
    [JsonPropertyName("day")]
    public int Day { get; set; }

    [JsonPropertyName("first_test_result")]
    public long FirstTestResult { get; set; }

    [JsonPropertyName("first_test_results")]
    public List<long> FirstTestResults { get; set; } = new ();


    [JsonPropertyName("second_test_input")]
    public string SecondTestInput { get; set; }
    
    [JsonPropertyName("second_test_inputs")]
    public List<string> SecondTestInputs { get; set; }

    [JsonPropertyName("second_test_result")]
    public long SecondTestResult { get; set; }
    
    [JsonPropertyName("second_test_results")]
    public List<long> SecondTestResults { get; set; }

    [JsonPropertyName("test_input")]
    public string TestInput { get; set; }

    [JsonPropertyName("test_inputs")]
    public List<string> TestInputs { get; set; } = new ();
}