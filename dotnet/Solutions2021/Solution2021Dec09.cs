namespace AdventOfCode.Solutions2021;

public class Solution2021Dec09 : Solution
{
    protected override long FirstSolution(List<string> lines)
    {
        var map = new Map(lines);
        return map.Features.Where(feature => feature.Type.Equals(FeatureType.Basin))
            .SelectMany(feature => feature.Points.Where(point => point.IsExtremePoint)
                .Select(point => point.RiskLevel))
            .ToList().Sum();
    }

    protected override long SecondSolution(List<string> lines)
    {
        var map = new Map(lines);
        map.AssignFeatureTypes();
        return map.Features.Where(feature => feature.Type.Equals(FeatureType.Basin))
            .OrderByDescending(feature => feature.Points.Count)
            .Take(3)
            .Select(feature => feature.Points.Count)
            .Product();
    }
}

internal class Point : LocationWithNeighbors
{
    public bool IsExtremePoint { get; set; }
    public Feature Feature { get; set; }

    public int Elevation => Value;

    public int RiskLevel => Elevation + 1;
}

public enum FeatureType
{
    Basin,
    Ridge
}

internal class Feature
{
    public List<Point> Points = new();
    public FeatureType Type;

    public Feature(FeatureType type, Point point)
    {
        Type = type;
        point.IsExtremePoint = true;
        point.Feature = this;
        Points.Add(point);
    }
}

internal class Map : MapWithNeighbors<Point>
{
    public HashSet<Feature> Features = new();

    public Map(List<string> lines) : base(lines)
    {
        FindExtremePoints();
    }

    public void FindExtremePoints()
    {
        var lowPoints = Locations.Values.Where(p => p.Elevation < p.Neighbors.Select(p => p.Value).Min()).ToList();
        lowPoints.ForEach(lowPoint => { Features.Add(new Feature(FeatureType.Basin, lowPoint)); });
        Locations.Values.Where(p => p.Elevation.Equals(9)).ToList().ForEach(highPoint =>
        {
            Features.Add(new Feature(FeatureType.Ridge, highPoint));
        });
    }

    public void AssignFeatureTypes()
    {
        var basins = Features.Where(feature => feature.Type.Equals(FeatureType.Basin));
        var elevation = basins.SelectMany(basin => basin.Points).Select(point => point.Elevation).Min();
        var points = Locations.Values.Where(point => point.Feature is null).ToHashSet();
        while (elevation < 9)
        {
            var pointsAtThisElevation = points.Where(point => point.Elevation.Equals(elevation)).ToList();
            while (pointsAtThisElevation.Count > 0)
            {
                pointsAtThisElevation = pointsAtThisElevation.Where(point => point.Feature is null).ToList();
                pointsAtThisElevation.ForEach(point =>
                {
                    var feature = point.Neighbors.Select(p => ((Point)p).Feature)
                        .FirstOrDefault(feature => feature is not null && feature.Type.Equals(FeatureType.Basin));
                    point.Feature = feature;
                    feature?.Points.Add(point);
                });
            }

            elevation++;
        }
    }
}