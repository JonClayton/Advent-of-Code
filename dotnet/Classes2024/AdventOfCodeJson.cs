using System.Text.Json.Serialization;

namespace OldAdventOfCode.Classes2024;

public class AdventOfCodeJson
{
    [JsonPropertyName("day")]
    public int Day { get; set; }

    [JsonPropertyName("input")]
    public string Input { get; set; }
    
    [JsonPropertyName("test_input")]
    public string TestInput { get; set; }
    
    [JsonPropertyName("test_1_result")]
    public long Test1Result { get; set; }

    [JsonPropertyName("test_2_result")]
    public long Test2Result { get; set; }
}