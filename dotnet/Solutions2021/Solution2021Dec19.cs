using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Cryptography;
using System.Xml;
using AdventOfCode.Utilities;
using Microsoft.VisualBasic;

namespace AdventOfCode.Solutions2021;

public class Solution2021Dec19 : Solution
{
    protected override long FirstSolution(List<string> lines)
    {
        var zone = new Zone();
        Scanner currentScanner = null;
        lines.ForEach(line =>
        {
            if (line.Length == 0) return;
            if (line[1] == '-')
            {
                currentScanner = new Scanner(line);
                zone.Add(currentScanner);
            }
            else currentScanner?.AddBeacon(line);
        });
        zone.RunFingerprints();
        zone.IdentifyFingerprintMatches();
        Console.WriteLine(zone.Scanners.Select(s => s.Beacons.Count).Sum());
        return 42;
    }

    protected override long SecondSolution(List<string> lines) => 5;


    private class Beacon
    {
        public (int, int, int) RelativeLocation;

        public Beacon(string line)
        {
            var nums = line.Split(",").Select(int.Parse).ToList();
            RelativeLocation = (nums[0], nums[1], nums[2]);
        }
    }

    private class FingerprintMatch
    {
        public Beacon Beacon1;
        public (int, int, int) Orientation1;
        public Beacon Beacon2;
        public (int, int, int) Orientation2;
    }

    private class Zone
    {
        public List<Scanner> Scanners = new();

        public void Add(Scanner scanner) => Scanners.Add(scanner);

        public void RunFingerprints() => Scanners.ForEach(s => s.RunFingerprints());

        public void IdentifyFingerprintMatches()
        {
            var fingerprintMatches = new List<FingerprintMatch>();
            var matchedBeacons = 0;
            for (var i = 0; i < Scanners.Count - 1; i++)
            {
                Console.WriteLine($"---------------- Scanner {i} -------------");
                for (var j = i + 1; j < Scanners.Count; j++)
                {
                    var maxOverlaps = 0;
                    var matches = Scanners[i].Fingerprints.ToList();
                    matches.Clear(); 
                    foreach (var fingerprint1 in Scanners[i].Fingerprints)
                    {
                        foreach (var fingerprint2 in Scanners[j].Fingerprints)
                        {
                            var overlaps = fingerprint1.Value.ToHashSet();
                            overlaps.IntersectWith(fingerprint2.Value);
                            if (overlaps.Count > maxOverlaps)
                            {
                                maxOverlaps = overlaps.Count;
                                matches.Clear();
                                matches.Add(fingerprint1);
                                matches.Add(fingerprint2);
                            }
                        }
                    }
                    Console.WriteLine($"{maxOverlaps}, {ConvertMatchesToSharedVertexCount(maxOverlaps)}");
                    matchedBeacons += ConvertMatchesToSharedVertexCount(maxOverlaps);
                }
            }
            Console.WriteLine(matchedBeacons);
        }

        private int ConvertMatchesToSharedVertexCount(int matches)
        {
            if (matches == 0) return 0;
            var count = 2;
            while (true)
            {
                var expectedMatches = (count - 1) * (count - 2) / 2;
                if (matches <= expectedMatches) return count -1;
                count++;
            }
        }
    }

    private class Scanner
    {
        public int Id;
        public List<Beacon> Beacons = new();

        private static List<(string, string, string)> DimensionPermutations = new()
        {
            ("x", "y", "z"),
            ("x", "z", "y"),
            ("y", "z", "x"),
            ("y", "x", "z"),
            ("z", "x", "y"),
            ("z", "y", "x")
        };
        
        private static List<(int, int, int)> RotationPermutations = new()
        {
            (1,1,1),
            (1,1,-1),
            (1,-1,1),
            (-1,1,1),
            (1,-1,-1),
            (-1,-1,1),
            (-1,1,-1),
            (-1,-1,-1)
        };

        public Dictionary<((string, string, string), (int, int, int)), HashSet<(int, int, int)>> Fingerprints =
            DimensionPermutations.SelectMany(d => 
                    RotationPermutations.Select(r => (d, r)))
                .ToDictionary(x => (x.d, x.r), x => new HashSet<(int, int, int)>());

        public Scanner(string line)
        {
            Id = int.Parse(line[12..^4]);
        }

        public void AddBeacon(string line)
        {
            Beacons.Add(new Beacon(line));
        }

        public void RunFingerprints()
        {
            for (var i = 0; i < Beacons.Count - 1; i++)
            {
                for (var j = i + 1; j < Beacons.Count; j++)
                {
                    var (x0, y0, z0) = Beacons[i].RelativeLocation;
                    var (x1, y1, z1) = Beacons[j].RelativeLocation;
                    var values = new Dictionary<string, int>()
                    {
                        { "x", x1 - x0 },
                        { "y", y1 - y0 },
                        { "z", z1 - z0 },
                    };
                    DimensionPermutations.ForEach(d =>
                        RotationPermutations.ForEach(r =>
                            Fingerprints[(d, r)].Add((values[d.Item1] * r.Item1, values[d.Item2] * r.Item2,
                                values[d.Item3] * r.Item3))));
                }
            }
        }
    }
}