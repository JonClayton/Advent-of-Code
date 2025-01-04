using System.Numerics;
using OldAdventOfCode.Utilities;

namespace OldAdventOfCode.Solutions2021;
// Orientations represent the possible alignments of Scanners
// The numbers represent changes in order of the axes, and the sign shows a sign change.
// For example, (1, 3, -2) means that (x, y, z) would be reported as (x, z, -y).
public class Orientation
{
// The reversal is the second in the pair and if applied to a transformed tuple will return it to original  
    private static readonly Dictionary<(int, int, int), (int, int, int)> OrientationsAndReversalTuples = new()
    {
        {(1, 2, 3), (1, 2, 3)},
        {(1, -3, 2), (1, 3, -2)},
        {(1, -2, -3), (1, -2, -3)},
        {(1, 3, -2), (1, -3, 2)},
        {(-1, -2, 3), (-1, -2, 3)},
        {(-1, -3, -2), (-1, -3, -2)},
        {(-1, 2, -3), (-1, 2, -3)},
        {(-1, 3, 2), (-1, 3, 2)},
        {(2, 1, -3), (2, 1, -3)},
        {(2, -3, -1), (-3, 1, -2)},
        {(2, -1, 3), (-2, 1, 3)},
        {(2, 3, 1), (3, 1, 2)},
        {(-2, 1, 3), (2, -1, 3)},
        {(-2, -3, 1), (3, -1, -2)},
        {(-2, -1, -3), (-2, -1, -3)},
        {(-2, 3, -1), (-3, -1, 2)},
        {(3, 2, -1), (-3, 2, 1)},
        {(3, 1, 2), (2, 3, 1)},
        {(3, -2, 1), (3, -2, 1)},
        {(3, -1, -2), (-2, -3, 1)},
        {(-3, 2, 1), (3, 2, -1)},
        {(-3, 1, -2), (2, -3, -1)},
        {(-3, -2, -1), (-3, -2, -1)},
        {(-3, -1, 2), (-2, 3, -1)}
    };

    internal static readonly Dictionary<Orientation, Orientation> OrientationsAndReversals =
        OrientationsAndReversalTuples.ToDictionary(
            x => new Orientation(x.Key),
            x => new Orientation(x.Value));
    public static readonly Orientation Default =
        OrientationsAndReversals.Single(x => x.Key._x == 1 && x.Key._y == 2 && x.Key._z == 3).Key;
    private readonly int _x;
    private readonly int _y;
    private readonly int _z;

    private Orientation((int, int, int) input)
    {
        var (item1, item2, item3) = input;
        _x = item1;
        _y = item2;
        _z = item3;
    }

    public Vector3 Apply(Vector3 vector)
    {
        var vectorList = new List<float> {vector.X, vector.Y, vector.Z};
        var orientationList = new List<float> {_x, _y, _z};
        var result = orientationList.Select(value =>
            {
                var abs = (int) Math.Abs(value);
                var sign = value / abs;
                return vectorList[abs - 1] * sign;
            })
            .ToList();
        return new Vector3(result[0], result[1], result[2]);
    }

    public Vector3 Reverse(Vector3 vector) => OrientationsAndReversals[this].Apply(vector);
}

public class Beacon
{
    public Vector3 Location;

    public Beacon(string line)
    {
        var nums = line.Split(",").Select(int.Parse).ToList();
        Location = new Vector3(nums[0], nums[1], nums[2]);
    }
}

// Edges are the line segments between Beacons detected by a scanner
// When multiple beacons are each detected by two different sensors,
// the reported segment between them will be the same,
// though transformed by the relative orientation of the two scanners
// The complete set of edges for a scanner is like a fingerprint that we need to match
public class Edge
{
    public readonly Beacon Beacon0;
    public readonly Beacon Beacon1;
    public Vector3 Segment;

    public Edge(Beacon beacon0, Beacon beacon1)
    {
        Beacon0 = beacon0;
        Beacon1 = beacon1;
        Segment = new Vector3(beacon1.Location.X - beacon0.Location.X, beacon1.Location.Y - beacon0.Location.Y,
            beacon1.Location.Z - beacon0.Location.Z);
    }

    public Edge GetReorientedEdge(Orientation orientation) => new(Beacon0, Beacon1) {Segment = orientation.Apply(Segment)};
}

public class Scanner
{
    public readonly List<Beacon> Beacons = new();
    public Vector3? Location;
    public bool IsMatchingComplete;

    private static readonly int MatchesRequiredForLocating = Enumerable.Range(0, 12).Sum();

    // these describe the geometry of the edges within the scanner's (initially unknown) orientation;
    private HashSet<Vector3> _edges = new();
    private readonly Dictionary<Edge, (Beacon, Beacon)> _edgeGeometry = new();

    // these describe the geometry of the edges once the beacons true locations are known,
    // subject to all the possible orientations
    private Dictionary<Orientation, HashSet<Vector3>> _relativeEdges = new();
    private readonly Dictionary<Orientation, Dictionary<Edge, (Beacon, Beacon)>> _relativeEdgeGeometry =
        Orientation.OrientationsAndReversals.Keys.ToDictionary(x => x, _ => new Dictionary<Edge, (Beacon, Beacon)>());
    
    // When we determine the scanner's location and its orientation we can set the location for the scanner
    // and all its beacons relative to the original scanner's orientation
    public void AddLocation(Orientation orientation, Vector3 location)
    {
        Beacons.ForEach(beacon =>
            beacon.Location = Vector3.Add(orientation.Reverse(beacon.Location), location));
        Location = location;
        CalculateBeaconGeometry();
    }

    // This could be called taking the fingerprints of the scanner.
    // Before we know the location we just want the line segments between the beacons.
    // After we know this scanner's location and have set the Beacon locations to absolute coordinates
    // we need all the edges as they would be described in each of the potential
    // orientations for the remaining scanners.
    // In one case or the other we need to include both A->B and B->A so we will match regardless
    // of the order in the other scanner, but if we did it in both we would get two matches for each segment
    // We choose to double the size of the first set since it is only one set instead of
    // 24 sets, one for each of the orientations, as this program is a memory hog 
    public void CalculateBeaconGeometry()
    {
        foreach (var beacon0 in Beacons)
        foreach (var beacon1 in Beacons.Where(beacon1 => !beacon0.Equals(beacon1)))
        {
            var edge = new Edge(beacon0, beacon1);
            if (Location != null)
                foreach (var orientation in Orientation.OrientationsAndReversals.Keys)
                {
                    if (Beacons.IndexOf(beacon1) < Beacons.IndexOf(beacon0)) continue;
                    var newEdge = edge.GetReorientedEdge(orientation);
                    _relativeEdgeGeometry[orientation].Add(newEdge, (beacon0, beacon1));
                }
            else _edgeGeometry.Add(edge, (beacon0, beacon1));
        }

        if (Location == null) _edges = _edgeGeometry.Keys.Select(k => k.Segment).ToHashSet();
        else
            _relativeEdges = _relativeEdgeGeometry.ToDictionary(x => x.Key,
                x => x.Value.Keys.Select(k => k.Segment).ToHashSet());
    }

    // Once we know we have a match, we can figure out the relative location of the target scanner
    // by comparing some of the matching edges
    private static void CalculateLocation(Scanner scanner, List<Vector3> matchingEdgeVectors,
        Orientation orientation, Dictionary<Edge, (Beacon, Beacon)> matchingEdgeGeometry)
    {
        // we need to look at two different edges because when we match with a single edge, we get two possible locations
        // since we don't know which beacon is which (call them A and B as seen by scanner 1 and a and b as seen by
        // scanner 2, we don't know if a is A or a is B.  So we look at another edge and use the location that is
        // one of the two options generated by the analysis for each of the two edges.
        var ((candidate0, candidate1), secondOptionSet) = GetTwoLocationCandidatePairs();
        scanner.AddLocation(orientation,
            secondOptionSet.Item1.Equals(candidate0) || secondOptionSet.Item2.Equals(candidate0)
                ? candidate0
                // we do the negation because the line segment was drawn from 0->1 in one scanner and 1->0 in the other
                : Vector3.Negate(candidate1));

        ((Vector3, Vector3), (Vector3, Vector3)) GetTwoLocationCandidatePairs()
        {
            var result = new List<Vector3>();
            for (var index = 0; index < 2; index++)
            {
                var edgeVector = matchingEdgeVectors[index];
                var found = scanner._edgeGeometry.Keys.Single(key => key.Segment == edgeVector);
                var known = matchingEdgeGeometry.Keys.Single(key => key.Segment == edgeVector);
                var beaconALocation = orientation.Reverse(found.Beacon0.Location);
                result.Add(Vector3.Subtract(known.Beacon0.Location, beaconALocation));
                // if the beacon isn't in the location above, it means the line connecting the beacon was drawn
                // in the opposite direction so our location vector needs a sign reversal
                var beaconAReversedLocation = orientation.Reverse(Vector3.Negate(found.Beacon1.Location));
                result.Add(Vector3.Subtract(known.Beacon0.Location, beaconAReversedLocation));
            }

            return ((result[0], result[1]), (result[2], result[3]));
        }
    }

    // We can see if there's enough matching beacons just by counting matching edges.  
    // Twelve matching beacons means 66 matching edges since adding the nth beacon adds n-1 new edges
    private void DetermineLocationIfPossible(Scanner scanner)
    {
        var scannerEdges = scanner._edges.ToList();
        var scannerMatchingEdgeVectors = new HashSet<Vector3>();
        foreach (var (orientation, edgeGeometry) in _relativeEdgeGeometry)
        {
            // this is a little indirect but it helps with a memory management issue
            scannerMatchingEdgeVectors.Clear();
            scannerEdges.ForEach(e => scannerMatchingEdgeVectors.Add(e));
            scannerMatchingEdgeVectors.IntersectWith(_relativeEdges[orientation]);
            if (scannerMatchingEdgeVectors.Count < MatchesRequiredForLocating) continue;
            CalculateLocation(scanner, scannerMatchingEdgeVectors.ToList(), orientation, edgeGeometry);
            break;
        }

        // Once we've compared this with all the remaining unmatched scanners, we can exclude it from future iterations
        IsMatchingComplete = true;
    }

    public void FindGeometryMatches(IEnumerable<Scanner> scanners) => scanners.ToList().ForEach(DetermineLocationIfPossible);
}

public class Solution2021Dec19 : Solution
{
    private List<Scanner> _scanners;

    protected override long FirstSolution(List<string> lines)
    {
        RunAnalysis(lines);
        return _scanners
            .SelectMany(scanner => scanner.Beacons)
            .Select(beacon => beacon.Location)
            .ToHashSet().Count;
    }

    protected override long SecondSolution(List<string> lines)
    {
        return (long) (
                from scanner0 in _scanners
                from scanner1 in _scanners
                select Vector3.Subtract(scanner0.Location.Value, scanner1.Location.Value))
            .Select(v => Math.Abs(v.X) + Math.Abs(v.Y) + Math.Abs(v.Z)).Max();
    }

    private void ReadInputToCreateScanners(IEnumerable<string> lines)
    {
        _scanners = new List<Scanner>();
        var currentScanner = new Scanner();
        lines.Where(line => line.Length > 0).ToList().ForEach(line =>
        {
            if (!'-'.Equals(line[1])) currentScanner?.Beacons.Add(new Beacon(line));
            else FinalizeCurrentScanner();
        });
        FinalizeCurrentScanner();
        _scanners.First().AddLocation(Orientation.Default, new Vector3(0, 0, 0));

        void FinalizeCurrentScanner()
        {
            if (!currentScanner.Beacons.Any()) return;
            currentScanner.CalculateBeaconGeometry();
            _scanners.Add(currentScanner);
            currentScanner = new Scanner();
        }
    }

    private void RunAnalysis(IEnumerable<string> lines)
    {
        ReadInputToCreateScanners(lines);
        while (_scanners.Any(s => s.Location is null))
            _scanners.Where(s => s.Location is not null && !s.IsMatchingComplete).ToList()
                .ForEach(s => s.FindGeometryMatches(_scanners.Where(scanner => scanner.Location is null)));
    }
}