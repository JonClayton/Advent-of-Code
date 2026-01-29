using System.Text.Json.Serialization;

namespace AdventOfCode.Tools;

public class Inputs<TType>
{
    public int Day { get; set; }

    [JsonPropertyName("puzzle_input")]
    public string PuzzleInput { get; init; } = string.Empty;

    [JsonPropertyName("test_input")]
    public string TestInput { get; init; } = string.Empty;

    [JsonPropertyName("test_1_input")]
    public string Test1Input { get; init; } = string.Empty;

    [JsonPropertyName("test_1_inputs")]
    public List<string> Test1Inputs { get; init; } = [];

    [JsonPropertyName("test_2_input")]
    public string Test2Input { get; init; } = string.Empty;

    [JsonPropertyName("test_2_inputs")]
    public List<string> Test2Inputs { get; init; } = [];

    [JsonPropertyName("test_1_result")]
    public TType? Test1Result { get; init; }

    [JsonPropertyName("test_1_results")]
    public List<TType> Test1Results { get; init; } = [];

    [JsonPropertyName("test_2_result")]
    public TType? Test2Result { get; init; }

    [JsonPropertyName("test_2_results")]
    public List<TType> Test2Results { get; init; } = [];
}