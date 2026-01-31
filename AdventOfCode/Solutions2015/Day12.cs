using System.Text.Json;
using System.Text.Json.Nodes;

namespace AdventOfCode.Solutions2015;

public class Day12 : Solution<long?>
{
    private bool _excludeRed;

    protected override long? FirstSolution(List<string> lines)
    {
        return GeneralSolution(lines);
    }

    protected override long? SecondSolution(List<string> lines)
    {
        _excludeRed = true;
        return GeneralSolution(lines);
    }

    private long GeneralSolution(List<string> lines)
    {
        return Value(JsonNode.Parse(lines.First())!);
    }

    private long Value(dynamic input)
    {
        return input switch
        {
            JsonObject jsonObject => ValueJsonObject(jsonObject),
            JsonArray jsonArray => jsonArray.Sum(Value!),
            _ => (input as JsonValue)!.GetValueKind() is JsonValueKind.Number
                ? (input as JsonValue)!.GetValue<long>()
                : 0
        };
    }

    private long ValueJsonObject(JsonObject jsonObject)
    {
        return _excludeRed && jsonObject.Any(property =>
            property.Value!.GetValueKind() is JsonValueKind.String && property.Value.GetValue<string>() == "red")
            ? 0
            : jsonObject.Sum(property => Value(property.Value!));
    }
}